
using Database.Models;
using Services.Users;
using Services.Resources;
using Models.Resources;

namespace Services.Loot {

    public class LootMiner {

        private readonly ILogger logger;

        private readonly LootTableIndex lootIndex;
        private readonly UserManager userManager;
        private readonly ResourceManager resourceManager;

        public LootMiner(ILogger<LootMiner> logger,
            LootTableIndex lootIndex,
            UserManager userManager,
            ResourceManager resourceManager) {
                
            this.logger = logger;
            
            this.lootIndex = lootIndex;
            this.userManager = userManager;
            this.resourceManager = resourceManager;
        }

        public async Task<NewResources> Mine(string userId, string nodeId) {

            logger.LogTrace("User: {userId} is trying to mine Node: {nodeId}...", userId, nodeId);

            //Generate and add some new resources
            List<ResourceStack> newResources = lootIndex.GenerateLoot(nodeId);
            List<ResourceStack> addedResources = await resourceManager.AddResources(userId, newResources);  //This value is currently unused

            //Get the current resource total
            List<ResourceStack> totalResources = await resourceManager.GetAllResources(userId);

            NewResources result = new(){
                NewResource = newResources,
                TotalResources = totalResources
            };

            logger.LogTrace("Finished mining resources for user: {userId}", userId);
            return result;
        }
    }
}