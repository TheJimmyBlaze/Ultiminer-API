
using Database;
using Database.Models;
using Services.Users;

namespace Services.Loot {

    public class LootMiner {

        private readonly ILogger logger;

        private readonly UltiminerContext database;
        private readonly UserManagement userManagement;

        public LootMiner(ILogger<LootMiner> logger,
            UltiminerContext database,
            UserManagement userManagement) {
                
            this.logger = logger;
            
            this.database = database;
            this.userManagement = userManagement;
        }

        public async Task<List<string>> Mine(string userId, string nodeId) {

            logger.LogTrace("User: {userId} is trying to mine Node: {nodeId}...", userId, nodeId);

            User user = await userManagement.GetUserForId(userId);
        }
    }
}