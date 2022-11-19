
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Nodes {

    public class NodeIndex {

        private readonly ILogger logger;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;

        //Key: Node Natural Id, Value: Whole Node object
        //The dictionary is ordered by required level
        private readonly Dictionary<string, Node> index = new();

        public NodeIndex(ILogger<NodeIndex> logger,
        IDbContextFactory<UltiminerContext> databaseFactory) {

            this.logger = logger;
            this.databaseFactory = databaseFactory;

            index = BuildIndex();
        }

        public Node? Get(string nodeId) {

            if (index.ContainsKey(nodeId)) {
                return index[nodeId];
            }

            return null;
        }

        public List<Node> GetForLevel(int level) {
            return index.Values
                .Where(node => node.LevelRequired <= level)
                .ToList();
        }

        public Node GetNextForLevel(int level) {
            return index.Values
                .First(node => node.LevelRequired > level);
        }

        private Dictionary<string, Node> BuildIndex() {

            logger.LogInformation("Building Node Indexes...");

            //Get db context
            using UltiminerContext database = databaseFactory.CreateDbContext();

            //Build
            Dictionary<string, Node> index = database.Nodes
                .OrderBy(node => node.LevelRequired)
                .ToDictionary(node => node.NaturalId, node => node);

            return index;
        }
    }
}