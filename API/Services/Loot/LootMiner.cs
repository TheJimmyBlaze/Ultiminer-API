
using Services.Users;
using Services.Resources;
using Models.Mining;
using Services.Stats;

namespace Services.Loot {

    public class LootMiner {

        private readonly ILogger logger;

        private readonly LootTableIndex lootIndex;
        private readonly ExperienceManager experienceManager;
        private readonly ResourceManager resourceManager;
        private readonly MiningStatsManager miningStats;

        public LootMiner(ILogger<LootMiner> logger,
            LootTableIndex lootIndex,
            ExperienceManager experienceManager,
            ResourceManager resourceManager,
            MiningStatsManager miningStats) {
                
            this.logger = logger;
            
            this.lootIndex = lootIndex;
            this.experienceManager = experienceManager;
            this.resourceManager = resourceManager;
            this.miningStats = miningStats;
        }

        public async Task<MiningResult> Mine(string userId, string nodeId) {

            logger.LogTrace("User: {userId} is trying to mine Node: {nodeId}...", userId, nodeId);

            //Generate and add some new resources
            List<ResourceStack> newResources = lootIndex.GenerateLoot(nodeId);
            List<ResourceStack> addedResources = await resourceManager.AddResources(userId, newResources);  //This value is currently unused

            //Award the experience
            int awardedExperience = ExperienceManager.SumResourceExperience(addedResources);
            int totalExperience = await experienceManager.AwardExperience(userId, awardedExperience);

            //Get the current resource total
            List<ResourceStack> totalResources = await resourceManager.GetAllResources(userId);

            //Update the stats and set the next mining time
            //TODO: replace this with a dynamic mining delay based on equipment and skills
            DateTime nextMine = DateTime.Now + TimeSpan.FromSeconds(2.5);
            await miningStats.Mine(userId, nextMine);

            MiningResult result = new(){
                Resources = new(){
                    NewResource = newResources,
                    TotalResources = totalResources
                },
                Exp = new(){
                    NewExp = awardedExperience,
                    TotalExp = totalExperience
                },
                NextMine = nextMine
            };

            logger.LogTrace("Finished mining resources for user: {userId}", userId);
            return result;
        }
    }
}