
using System.Diagnostics;
using System.Linq.Expressions;
using Database;
using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services.DropTables {

    public class DropTableIndex {

        private readonly ILogger<DropTableIndex> logger;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;
        
        private readonly Dictionary<string, Tuple<int, List<Tuple<int, string>>>> indexes = new();

        public DropTableIndex(ILogger<DropTableIndex> logger, IDbContextFactory<UltiminerContext> databaseFactory) {
            this.logger = logger;
            this.databaseFactory = databaseFactory;

            BuildIndex();
        }

        public void BuildIndex() {

            //Reset the index dictionary
            indexes.Clear();

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

                logger.LogInformation("Building Drop table Index for Node: {DisplayName}", node.DisplayName);
                Stopwatch nodeTimer = new();
                nodeTimer.Start();

                //Do stuff

                //Just do a single table for now
                foreach(NodeDropTable table in node.DropTables) {

                    List<Tuple<int, string>> tableIndex = new();

                    IEnumerable<DropTableResource> resources = table.DropTable.Resources.OrderBy(resource => resource.Rarity);
                    int maxResourceIndex = resources.Count() -1;
                    for(int i = 0; i <= maxResourceIndex; i++) {

                        string resourceId = resources.ElementAt(i).ResourceId;
                        int baseRarity = resources.ElementAt(maxResourceIndex - i).Rarity;
                        int additiveRarity = tableIndex.LastOrDefault()?.Item1 ?? 0;
                        int rarity = baseRarity + additiveRarity;
                        tableIndex.Add(new(rarity, resourceId));
                    }

                    indexes.Add(node.NaturalId + "." + table.DropTableId, new(0, tableIndex));
                }

                nodeTimer.Stop();
                logger.LogInformation("Finished building Drop Table Index for Node: {DisplayName} after: {NodeTimerMS}ms", node.DisplayName, nodeTimer.ElapsedMilliseconds);
            }

            totalTimer.Stop();
            logger.LogInformation("Finished building all Drop Table Indexes after: {TotalTimerMS}ms", totalTimer.ElapsedMilliseconds);
        }
    }
}