
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Models.Mining;

namespace Services.Experience {

    public class ExperienceManager {

        private readonly ILogger logger;

        private readonly ResourceExperienceIndex index;

        private readonly UltiminerContext database;

        public ExperienceManager(ILogger<ExperienceManager> logger,
            ResourceExperienceIndex index,
            UltiminerContext database) {
            
            this.logger = logger;
            this.index = index;
            this.database = database;
        }

        public async Task<int> AwardExperience(string userId, int experienceAwarded) {

            logger.LogTrace("Awarding: {experience} experience to user: {userId}...", userId, experienceAwarded);
            
            //Get the existing user experience if it exists
            UserLevel? userExp = await database.UserLevel
                .FirstOrDefaultAsync(exp => exp.UserId == userId);


            //If no experience exists for this user, create some
            if (userExp == null) {

                userExp = new (){
                    UserId = userId,
                };
                database.UserLevel.Add(userExp);
            }

            //Add the new xp :)
            userExp.TotalExperience += experienceAwarded;
            //TODO: Add level and level xp

            //Save the new exp
            await database.SaveChangesAsync();
            logger.LogTrace("User: {userId} has a total of: {totalExperience} exp", userId, userExp.TotalExperience);

            return userExp.TotalExperience;
        }

        public int SumResourceExperience(List<ResourceStack> resources) {

            //Reduce resources to their experience values, and sum them
            return resources.Select(resource => {
                return index.Get(resource.ResourceId) * resource.Count;
            }).Sum();
        }
    }
}