
using Services.Experience;
using Services.Nodes;
using Services.Resources;
using Services.Stats;
using Models.Mining;
using Models.Resources;
using Models.Experience;
using Exceptions;

namespace Services.Loot {

    public class LootMiner {

        private readonly ILogger logger;

        private readonly LootTableIndex lootIndex;
        private readonly ExperienceManager experienceManager;
        private readonly NodeIndex nodeIndex;
        private readonly NodeManager nodeManager;
        private readonly ResourceManager resourceManager;
        private readonly MiningStatsManager miningStats;

        public LootMiner(ILogger<LootMiner> logger,
            LootTableIndex lootIndex,
            ExperienceManager experienceManager,
            NodeIndex nodeIndex,
            NodeManager nodeManager,
            ResourceManager resourceManager,
            MiningStatsManager miningStats) {
                
            this.logger = logger;
            
            this.lootIndex = lootIndex;
            this.experienceManager = experienceManager;
            this.nodeIndex = nodeIndex;
            this.nodeManager = nodeManager;
            this.resourceManager = resourceManager;
            this.miningStats = miningStats;
        }

        public async Task<MiningResult> Mine(string userId) {

            logger.LogTrace("User: {userId} is attempting to mine...", userId);

            //Check if they can mine, if they can't return 'resource exhausted' error code
            if (! await miningStats.CanMine(userId)) {
                throw new TooManyRequestsException($"User: {userId} next mine time has not elapsed");
            }

            //Get the selected node
            string nodeId = nodeManager.GetSelectedNode(userId);

            //Generate and add some new resources
            List<ResourceStack> newResources = lootIndex.GenerateLoot(nodeId);
            List<ResourceStack> addedResources = await resourceManager.AddResources(userId, newResources);  //This value is currently unused

            //Award the experience
            int awardedExperience = experienceManager.SumResourceExperience(addedResources);
            NewExperience newExperience = await experienceManager.AwardExperience(userId, awardedExperience);

            //Get the current resource total
            List<ResourceStack> totalResources = await resourceManager.GetAllResources(userId);

            //Update the stats and set the next mining time
            //TODO: replace this with a dynamic mining delay based on equipment and skills
            DateTime nextMine = DateTime.UtcNow + TimeSpan.FromSeconds(2.5);
            await miningStats.Mine(userId, nextMine);

            MiningResult result = new(){
                Resources = new(){
                    NewResource = newResources,
                    TotalResources = totalResources
                },
                Exp = newExperience,
                NextMine = nextMine
            };

            logger.LogTrace("Finished mining resources for user: {userId}", userId);
            return result;
        }
    }
}