
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Models.Resources;

namespace Services.Resources {

    public class ResourceManager {

        private readonly ILogger logger;
        private readonly UltiminerContext database;

        public ResourceManager(ILogger<ResourceManager> logger,
            UltiminerContext database) {

            this.logger = logger;
            this.database = database;
        }

        public async Task<List<ResourceStack>> GetAllResources(string userId) {

            logger.LogTrace("Getting all resources for user: {userId}...", userId);

            List<ResourceStack> resources = await database.UserResources
                .Where(resource => resource.UserId == userId)
                .Include(resource => resource.Resource)
                .Select(resource => new ResourceStack(){
                    ResourceId = resource.ResourceId,
                    DisplayName = resource.Resource.DisplayName,
                    Type = resource.Resource.ResourceTypeId,
                    Count = resource.Count
                })
                .ToListAsync();

            logger.LogTrace("Got: {count} resources for user: {userId}", resources.Count, userId);
            return resources;
        }

        public List<ResourceStack> CullUnknownResources(List<ResourceStack> resources){

            logger.LogTrace("Culling unknown resources...");
            
            IEnumerable<string> stackResourceIds = resources.Select(resource => resource.ResourceId);
            IEnumerable<string> knownResourceIds = database.Resources
                .Select(resource => resource.NaturalId)
                .Where(resource => stackResourceIds.Contains(resource));

            IEnumerable<ResourceStack> knownResources = resources.Where(resource => knownResourceIds.Contains(resource.ResourceId));
            int culled = resources.Count - knownResourceIds.Count();

            logger.LogTrace("Culled: {count}", culled);
            return knownResources.ToList();
        }

        public async Task<List<ResourceStack>> AddResources(string userId, List<ResourceStack> resources) {

            logger.LogTrace("Adding resources to user: {userId}...", userId);

            //Remove any bad resources
            List<ResourceStack> knownResources = CullUnknownResources(resources);

            //Add each resource in the stack
            int newResources = 0;
            int existingResources = 0;
            foreach(ResourceStack stack in knownResources) {
                
                //Add any resources that don't exist, increment those that do
                UserResource? resource = await database.UserResources
                    .FirstOrDefaultAsync(resource => resource.UserId == userId && resource.ResourceId == stack.ResourceId);
                
                if (resource == null) {

                    resource = new UserResource(){
                        UserId = userId,
                        ResourceId = stack.ResourceId,
                        Count = stack.Count
                    };
                    database.UserResources.Add(resource);
                    newResources += stack.Count;
                } else {
                    
                    resource.Count += stack.Count;
                    existingResources += stack.Count;
                }
            }

            //Save the resource modifications
            await database.SaveChangesAsync();

            //Return the resources we've added
            logger.LogTrace("Added: {new} new resources, Incremented: {incremented} existing resources", newResources, existingResources);
            return knownResources;
        }
    }
}