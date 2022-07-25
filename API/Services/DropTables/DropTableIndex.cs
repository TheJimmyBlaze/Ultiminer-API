
using System.Diagnostics;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.DropTables {

    public class DropTableIndex {

        private readonly ILogger<DropTableIndex> logger;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;
        
        private readonly Dictionary<string, List<Tuple<float, string>>> index = new();

        public DropTableIndex(ILogger<DropTableIndex> logger, IDbContextFactory<UltiminerContext> databaseFactory) {
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

                //Just one table at a time till I actually get this to word :,(
                foreach(NodeDropTable table in node.DropTables) {

                    IEnumerable<DropTableResource> resources = table.DropTable.Resources;

                    //Sort by descending, this is important for later triangular form
                    IEnumerable<DropTableResource> sorted = resources.OrderByDescending(resource => resource.Rarity);

                    //Build a weight index for the table
                    Dictionary<int, int> weightIndex = BuildWeightIndex(sorted);

                    //Replace each rarity with it's indexed weight
                    IEnumerable<int> weights = sorted.Select(resource => weightIndex[resource.Rarity]);

                    //Convert each weight to a percentage
                    float weightSum = weights.Sum();
                    IEnumerable<float> percentages = weights.Select(weight => weight / weightSum);

                    //Convert percentages to triangular form, optimizing later drop calculation
                    IEnumerable<float> slidingSum = percentages.Prepend(0).Zip(percentages, (current, previous) => current + previous);
                    IEnumerable<float> triangular = slidingSum.Prepend(0).Zip(percentages, (slidingSum, percentage) => slidingSum + percentage);

                    //Create the percentage index by pairing the triangular percentage with it's resource ID
                    IDictionary<float, string> index = triangular.Zip(sorted, (triangular, resource) => new KeyValuePair<float, string>(triangular, resource.ResourceId))
                        .ToDictionary(index => index.Key, index => index.Value);
                }
            }

            totalTimer.Stop();
            logger.LogInformation("Finished building all Drop Table Indexes after: {TotalTimerMS}", totalTimer.Elapsed);
        }

        private static Dictionary<int, int> BuildWeightIndex(IEnumerable<DropTableResource> resources) {

            //Get each unique rarity on the table
            IEnumerable<int> distinct = resources
                .Select(resource => resource.Rarity)
                .Distinct();

            //Invert the rarities by subtracting them from the sum
            int raritySum = distinct.Sum();
            IEnumerable<int> inverted = distinct.Select(rarity => raritySum - rarity);

            //Convert the set of numbers to triangular form, each number summed with it's predecessors
            IEnumerable<int> slidingSum = inverted.Prepend(0).Zip(inverted, (current, previous) => current + previous);
            IEnumerable<int> triangular = slidingSum.Prepend(0).Zip(inverted, (slidingSum, inverted) => slidingSum + inverted);

            //Create the weight index by pairing the triangular form with it's original value
            Dictionary<int, int> weightIndex = triangular.Zip(distinct, (triangular, original) => new KeyValuePair<int, int>(original, triangular))
                .ToDictionary(index => index.Key, index => index.Value);

            return weightIndex;
        }
    }
}