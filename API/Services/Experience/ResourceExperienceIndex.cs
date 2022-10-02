
using Database;
using Microsoft.EntityFrameworkCore;

namespace Services.Experience {

    public class ResourceExperienceIndex {

        private readonly ILogger logger;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;

        //Key: Resource Natural Id, Value: Experience Reward
        private readonly Dictionary<string, int> index = new();

        public ResourceExperienceIndex(ILogger<ResourceExperienceIndex> logger,
            IDbContextFactory<UltiminerContext> databaseFactory) {
            
            this.logger = logger;
            this.databaseFactory = databaseFactory;

            index = BuildIndex();
        }

        public int Get(string resourceId) {
            return index[resourceId];
        }

        private Dictionary<string, int> BuildIndex() {

            logger.LogInformation("Building Resource Experience Indexes...");

            //Get a db context
            using UltiminerContext database = databaseFactory.CreateDbContext();

            //Build index
            Dictionary<string, int> index = database.Resources
                .ToDictionary(resource => resource.NaturalId, resource => resource.ExperienceAwarded);
        
            return index;
        }
    }
}