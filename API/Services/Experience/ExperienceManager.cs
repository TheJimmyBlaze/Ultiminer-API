
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Models.Resources;
using Models.Experience;
using Services.Resources;

namespace Services.Experience {

    public class ExperienceManager {

        private readonly ILogger logger;

        private readonly LevelExperienceIndex levelIndex;
        private readonly ResourceIndex resourceIndex;

        private readonly UltiminerContext database;

        public ExperienceManager(ILogger<ExperienceManager> logger,
            LevelExperienceIndex levelIndex,
            ResourceIndex resourceIndex,
            UltiminerContext database) {
            
            this.logger = logger;
            this.levelIndex = levelIndex;
            this.resourceIndex = resourceIndex;
            this.database = database;
        }

        public async Task<ExperienceTotal> GetExperience(string userId) {

            logger.LogTrace("Getting total experience for user: {userId}", userId);

            //Get the existing user experience if it exists
            UserLevel? userExp = await database.UserLevel
                .FirstOrDefaultAsync(exp => exp.UserId == userId);

            //Create the experience response
            ExperienceTotal response = new();
            if (userExp != null) {

                response.Level = userExp.Level;
                response.Experience = userExp.LevelExperience;
                response.NextLevelExperience = levelIndex.Get(userExp.Level + 1);
            }

            return response;
        }

        public async Task<NewExperience> AwardExperience(string userId, int awardedExp) {

            logger.LogTrace("Awarding: {experience} experience to user: {userId}...", userId, awardedExp);
            
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
            userExp.TotalExperience += awardedExp;
            
            //While the users levelExp + awardedExp > nextLevelExp, increment their level
            int levelExp = userExp.LevelExperience + awardedExp;
            int nextLevelExp = levelIndex.Get(userExp.Level + 1);
            while(levelExp >= nextLevelExp) {
                
                //Increment level
                userExp.Level ++;
                //Recalculate levelExp
                levelExp = levelExp - nextLevelExp;
                //Recalculate nextLevelExp
                nextLevelExp = levelIndex.Get(userExp.Level + 1);

                logger.LogTrace("User: {userId} has leveled up, they are now: {level}, with: {levelExp} to the next level", userId, userExp.Level, levelExp);
            }
            //Update the xp after applying level ups
            userExp.LevelExperience = levelExp;

            //Save the new exp
            await database.SaveChangesAsync();
            logger.LogTrace("User: {userId} has a total of: {totalExperience} exp", userId, userExp.TotalExperience);

            return new(){
                NewExp = awardedExp,
                Level = userExp.Level,
                Experience = userExp.LevelExperience,
                NextLevelExperience = nextLevelExp
            };
        }

        public int SumResourceExperience(List<ResourceStack> resources) {

            //Reduce resources to their experience values, and sum them
            return resources.Select(resource => {
                int resourceExp = resourceIndex.Get(resource.ResourceId)!.ExperienceAwarded;
                return  resourceExp * resource.Count;
            }).Sum();
        }
    }
}