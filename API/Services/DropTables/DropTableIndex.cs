
using System.Diagnostics;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.DropTables {

    public class DropTableIndex {

        private readonly ILogger<DropTableIndex> logger;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;
        
        private readonly Dictionary<string, List<Tuple<float, string>>> cache = new();

        public DropTableIndex(ILogger<DropTableIndex> logger, IDbContextFactory<UltiminerContext> databaseFactory) {
            this.logger = logger;
            this.databaseFactory = databaseFactory;

            BuildIndex();
        }

        public void BuildIndex() {

            //Reset the index dictionary
            cache.Clear();

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

                List<Tuple<float, string>> nodeTable = new();

                logger.LogInformation("Building Drop table Index for Node: {DisplayName}", node.DisplayName);
                Stopwatch nodeTimer = new();
                nodeTimer.Start();

                IEnumerable<NodeDropTable> tables = node.DropTables.OrderByDescending(table => table.TableRarity);
                int maxTableIndex = tables.Count() -1;
                float totalTableRarity = tables.Sum(table => table.TableRarity);

                for(int tableIndex = 0; tableIndex <= maxTableIndex; tableIndex++) {

                    DropTable table = tables.ElementAt(tableIndex).DropTable;
                    float tableRarity = tables.ElementAt(maxTableIndex - tableIndex).TableRarity;
                    float tableRarityPercent = tableRarity / totalTableRarity;

                    IEnumerable<DropTableResource> resources = table.Resources.OrderByDescending(resource => resource.Rarity);
                    int maxResourceIndex = resources.Count() -1;
                    float totalResourceRarity = resources.Sum(resource => resource.Rarity);

                    for(int resourceIndex = 0; resourceIndex <= maxResourceIndex; resourceIndex++) {

                        string resourceId = resources.ElementAt(resourceIndex).ResourceId;
                        float resourceRarity = resources.ElementAt(maxResourceIndex - resourceIndex).Rarity;
                        float resourceRarityPercent = resourceRarity / totalResourceRarity;

                        float rarity = resourceRarityPercent * tableRarityPercent;
                        nodeTable.Add(new(rarity, resourceId));
                    }
                }

                cache.Add(node.NaturalId, nodeTable);
                logger.LogInformation("{Node} Cache Sum: {Sum}", node.NaturalId, nodeTable.Sum(index => index.Item1));

                nodeTimer.Stop();
                logger.LogInformation("Finished building Drop Table Index for Node: {DisplayName} after: {NodeTimerMS}", node.DisplayName, nodeTimer.Elapsed);
            }

            totalTimer.Stop();
            logger.LogInformation("Finished building all Drop Table Indexes after: {TotalTimerMS}", totalTimer.Elapsed);
        }
    }
}