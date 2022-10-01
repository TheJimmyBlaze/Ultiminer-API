
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Models.Mining;

namespace Services.Users {

    public class ExperienceManager {

        private readonly ILogger logger;

        private readonly UltiminerContext database;

        public ExperienceManager(ILogger<ExperienceManager> logger,
            UltiminerContext database) {
            
            this.logger = logger;

            this.database = database;
        }

        public async Task<int> AwardExperience(string userId, int experienceAwarded) {

            logger.LogTrace("Awarding: {experience} experience to user: {userId}...", userId, experienceAwarded);
            
            //Get the existing user experience if it exists
            Experience? userExp = await database.Experience
                .FirstOrDefaultAsync(exp => exp.UserId == userId);

            if (userExp == null) {

                //If no experience exists for this user, create some
                userExp = new (){
                    UserId = userId,
                    TotalExperience = experienceAwarded
                };
                database.Experience.Add(userExp);
            } else {

                //If a user already has exp, add some more :)
                userExp.TotalExperience += experienceAwarded;
            }

            //Save the new exp
            await database.SaveChangesAsync();
            logger.LogTrace("User: {userId} has a total of: {totalExperience} exp", userId, userExp.TotalExperience);

            return userExp.TotalExperience;
        }

        public static int SumResourceExperience(List<ResourceStack> resources) {

            //Reduce resources to their experience values, and sum them
            return resources.Select(resource => {
                return resource.ExperienceAwarded * resource.Count;
            }).Sum();
        }
    }
}