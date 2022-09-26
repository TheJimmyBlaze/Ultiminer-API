
using Database.Models;
using Services.Users;
using Services.Resources;
using Models.Resources;
using Services.Stats;

namespace Services.Loot {

    public class LootMiner {

        private readonly ILogger logger;

        private readonly LootTableIndex lootIndex;
        private readonly ResourceManager resourceManager;
        private readonly MiningStatsManager miningStats;

        public LootMiner(ILogger<LootMiner> logger,
            LootTableIndex lootIndex,
            ResourceManager resourceManager,
            MiningStatsManager miningStats) {
                
            this.logger = logger;
            
            this.lootIndex = lootIndex;
            this.resourceManager = resourceManager;
            this.miningStats = miningStats;
        }

        public async Task<NewResources> Mine(string userId, string nodeId) {

            logger.LogTrace("User: {userId} is trying to mine Node: {nodeId}...", userId, nodeId);

            //Generate and add some new resources
            List<ResourceStack> newResources = lootIndex.GenerateLoot(nodeId);
            List<ResourceStack> addedResources = await resourceManager.AddResources(userId, newResources);  //This value is currently unused

            //Get the current resource total
            List<ResourceStack> totalResources = await resourceManager.GetAllResources(userId);

            //Update the stats and set the next mining time
            //TODO: replace this with a dynamic mining delay based on equipment and skills
            DateTime nextMine = DateTime.Now + TimeSpan.FromSeconds(2.5);
            await miningStats.Mine(userId, nextMine);

            NewResources result = new(){
                NewResource = newResources,
                TotalResources = totalResources,
                NextMine = nextMine
            };

            logger.LogTrace("Finished mining resources for user: {userId}", userId);
            return result;
        }
    }
}