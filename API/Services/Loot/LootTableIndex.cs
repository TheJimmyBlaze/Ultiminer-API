
using System.Diagnostics;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Models.Mining;

namespace Services.Loot {

    public class LootTableIndex {

        private readonly ILogger<LootTableIndex> logger;
        private readonly Random random;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;

        private struct ResourceIndex {
            public string NaturalId {get; set;}
            public int Exp {get; set;}
        }

        private struct NodeIndex {
            public int Quantity {get; set;}
            public IDictionary<double, ResourceIndex> Index {get; set;}
        }
        private readonly Dictionary<string, NodeIndex> index = new();

        public LootTableIndex(ILogger<LootTableIndex> logger, 
            Random random,
            IDbContextFactory<UltiminerContext> databaseFactory) {

            this.logger = logger;
            this.random = random;
            this.databaseFactory = databaseFactory;

            BuildIndex();
        }

        public List<ResourceStack> GenerateLoot(string nodeId) {

            logger.LogTrace("Generating Loot for Node: {nodeId}...", nodeId);

            if (index.TryGetValue(nodeId, out NodeIndex nodeIndex)) {
                
                //Store it as a dictionary during generation, this makes it easier to increment a resource by id
                Dictionary<ResourceIndex, int> rawLoot = new();

                int quantity = random.Next(nodeIndex.Quantity);
                for(int roll = 0; roll <= quantity; roll++) {

                    double lootKey = random.NextDouble();
                    ResourceIndex resource = nodeIndex.Index.First(index => index.Key >= lootKey).Value;

                    if (!rawLoot.ContainsKey(resource)) {
                        rawLoot[resource] = 1;
                    } else {
                        rawLoot[resource] ++;
                    }
                }

                //Convert the dictionary to a list of Resource Stacks
                List<ResourceStack> resources = rawLoot.Select(raw => new ResourceStack(){
                    ResourceId = raw.Key.NaturalId,
                    ExperienceAwarded = raw.Key.Exp,
                    Count = raw.Value
                }).ToList();

                logger.LogTrace("Generated: {lootCount} bits of loot", resources.Count);
                return resources;
            }

            logger.LogDebug("Error generating loot: Node: {nodeId} doesn't exist", nodeId);
            throw new ArgumentException("Invalid Node Id: {0}", nodeId);
        }

        public void BuildIndex() {

            //Reset the index dictionary
            index.Clear();

            logger.LogInformation("Building Loot Table Indexes...");
            Stopwatch totalTimer = new();
            totalTimer.Start();

            //Get a db context
            using UltiminerContext database = databaseFactory.CreateDbContext();

            //Build the exp index, we'll use this when we finish up each index
            Dictionary<string, int> expIndex = buildExperienceIndex(database);

            //Build the index for every node
            IEnumerable<Node> nodes = database.Nodes
                .Include(node => node.LootTables)
                .ThenInclude(table => table.LootTable)
                .ThenInclude(table => table.Resources);

            foreach(Node node in nodes) {

                //Collapse each table into a single set
                List<KeyValuePair<string, int>> resources = new();
                foreach(NodeLootTable table in node.LootTables) {

                    //Include only eligible resources for this node
                    IEnumerable<LootTableResource> eligibleResources = table.LootTable.Resources
                        .Where(resource => resource.Rarity >= table.MinRarity && resource.Rarity <= table.MaxRarity);

                    //Resolve each resources rarity by multiplying it by it's table's rarity
                    int tableRarity = table.TableRarity;
                    IEnumerable<KeyValuePair<string, int>> tableResources = eligibleResources
                        .Select(resource => new KeyValuePair<string, int>(resource.ResourceId, resource.Rarity * tableRarity));
                    resources.AddRange(tableResources);
                }

                //Sort by descending, this is important for later triangular form
                IEnumerable<KeyValuePair<string, int>> sorted = resources.OrderByDescending(resource => resource.Value);

                //Build a weight index for the table
                IEnumerable<int> rawRarities = sorted.Select(rarity => rarity.Value);
                Dictionary<int, int> weightIndex = buildWeightIndex(rawRarities);

                //Replace each rarity with it's indexed weight
                IEnumerable<int> weights = sorted.Select(resource => weightIndex[resource.Value]);

                //Convert each weight to a percentage
                double weightSum = weights.Sum();
                IEnumerable<double> percentages = weights.Select(weight => weight / weightSum);

                //Convert percentages to triangular form, optimizing later loot calculation
                IEnumerable<double> triangular = percentages.Select((_, i) => percentages.Take(i + 1).Sum());

                //Create the percentage index by pairing the triangular percentage with it's resource ID
                IDictionary<double, ResourceIndex> indexData = triangular
                    .Zip(sorted, (triangular, resource) => new KeyValuePair<double, string>(triangular, resource.Key))
                    .ToDictionary(index => index.Key, index => {
                        return new ResourceIndex(){
                            NaturalId = index.Value,
                            Exp = expIndex[index.Value]
                        };
                    });

                //Add the node index to the loot index
                NodeIndex nodeIndex = new(){
                    Quantity = node.Quantity,
                    Index = indexData
                };
                index.Add(node.NaturalId, nodeIndex);
            }

            totalTimer.Stop();
            logger.LogInformation("Finished building all Loot Table Indexes after: {TotalTimerMS}", totalTimer.Elapsed);
        }

        private static Dictionary<int, int> buildWeightIndex(IEnumerable<int> rarities) {

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

        private static Dictionary<string, int> buildExperienceIndex(UltiminerContext database) {

            //Get a dictionary where the key is the resource Id, and the value is the experience awarded
            Dictionary<string, int> expIndex = database.Resources
                .Select(resource => new KeyValuePair<string, int>(resource.NaturalId, resource.ExperienceAwarded))
                .ToDictionary(resource => resource.Key, resource => resource.Value);

            return expIndex;
        }
    }
}