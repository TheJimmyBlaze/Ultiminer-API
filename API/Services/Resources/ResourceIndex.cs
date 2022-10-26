
using Database;
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Services.Resources {

    public class ResourceIndex {

        private readonly ILogger logger;
        private readonly IDbContextFactory<UltiminerContext> databaseFactory;

        //Key: Resource Natural Id, Value: Whole resource object
        private readonly Dictionary<string, Resource> index = new();

        public ResourceIndex(ILogger<ResourceIndex> logger,
            IDbContextFactory<UltiminerContext> databaseFactory) {
            
            this.logger = logger;
            this.databaseFactory = databaseFactory;

            index = BuildIndex();
        }

        public Resource Get(string resourceId) {
            return index[resourceId];
        }

        private Dictionary<string, Resource> BuildIndex() {

            logger.LogInformation("Building Resource Experience Indexes...");

            //Get a db context
            using UltiminerContext database = databaseFactory.CreateDbContext();

            //Build index
            Dictionary<string, Resource> index = database.Resources
                .ToDictionary(resource => resource.NaturalId, resource => resource);
        
            return index;
        }
    }
}