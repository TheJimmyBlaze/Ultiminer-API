
using System.Diagnostics;
using System.Security.AccessControl;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace Services.Loot {

    public class LootTableIndex {

        private readonly ILogger<LootTableIndex> logger;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;
        
        private readonly Dictionary<string, List<Tuple<float, string>>> index = new();

        public LootTableIndex(ILogger<LootTableIndex> logger, IDbContextFactory<UltiminerContext> databaseFactory) {
            this.logger = logger;
            this.databaseFactory = databaseFactory;

            BuildIndex();
        }

        public void BuildIndex() {

            //Reset the index dictionary
            index.Clear();

            logger.LogInformation("Building Drop Table Indexes...");
            Stopwatch totalTimer = new();
            totalTimer.Start();

            //Get a db context
            using UltiminerContext database = databaseFactory.CreateDbContext();

            //Build the index for every node
            IEnumerable<Node> nodes = database.Nodes
                .Include(node => node.DropTables)
                .ThenInclude(table => table.DropTable)
                .ThenInclude(table => table.Resources);

            foreach(Node node in nodes) {

                //Collapse each table into a single set
                List<KeyValuePair<string, int>> resources = new();
                foreach(NodeDropTable table in node.DropTables) {

                    //Resolve each resources rarity by multiplying it by it's table's rarity
                    int tableRarity = table.TableRarity;
                    IEnumerable<KeyValuePair<string, int>> tableResources = table.DropTable.Resources
                        .Select(resource => new KeyValuePair<string, int>(resource.ResourceId, resource.Rarity * tableRarity));
                    resources.AddRange(tableResources);
                }

                //Sort by descending, this is important for later triangular form
                IEnumerable<KeyValuePair<string, int>> sorted = resources.OrderByDescending(resource => resource.Value);

                //Build a weight index for the table
                IEnumerable<int> rawRarities = sorted.Select(rarity => rarity.Value);
                Dictionary<int, int> weightIndex = BuildWeightIndex(rawRarities);

                //Replace each rarity with it's indexed weight
                IEnumerable<int> weights = sorted.Select(resource => weightIndex[resource.Value]);

                //Convert each weight to a percentage
                float weightSum = weights.Sum();
                IEnumerable<float> percentages = weights.Select(weight => weight / weightSum);

                //Convert percentages to triangular form, optimizing later drop calculation
                IEnumerable<float> triangular = percentages.Select((_, i) => percentages.Take(i + 1).Sum());

                //Create the percentage index by pairing the triangular percentage with it's resource ID
                IDictionary<float, string> index = triangular
                    .Zip(sorted, (triangular, resource) => new KeyValuePair<float, string>(triangular, resource.Key))
                    .ToDictionary(index => index.Key, index => index.Value);
            }

            totalTimer.Stop();
            logger.LogInformation("Finished building all Drop Table Indexes after: {TotalTimerMS}", totalTimer.Elapsed);
        }

        private static Dictionary<int, int> BuildWeightIndex(IEnumerable<int> rarities) {

            //Get each unique rarity on the table
            IEnumerable<int> distinct = rarities.Distinct();

            //Invert the rarities by subtracting them from the sum
            int raritySum = distinct.Sum();
            IEnumerable<int> inverted = distinct.Select(rarity => raritySum - rarity);

            //Convert the set of numbers to triangular form, each number summed with it's predecessors
            IEnumerable<int> triangular = inverted.Select((_, i) => inverted.Take(i + 1).Sum());

            //Create the weight index by pairing the triangular form with it's original value
            Dictionary<int, int> weightIndex = triangular.Zip(distinct, (triangular, original) => new KeyValuePair<int, int>(original, triangular))
                .ToDictionary(index => index.Key, index => index.Value);

            return weightIndex;
        }
    }
}