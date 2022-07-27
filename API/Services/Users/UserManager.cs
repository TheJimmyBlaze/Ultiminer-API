
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Users {

    public class UserManager {

        private readonly ILogger logger;

        private readonly UltiminerContext database;

        public UserManager(ILogger<UserManager> logger,
            UltiminerContext database) {
            
            this.logger = logger;

            this.database = database;
        }

        public async Task EnsureUserExits(string userId) {

            logger.LogTrace("Ensuring user: {userId} exists...", userId);

            //Get a user if one exists
            User? user = await database.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            if (user == null) {
                
                logger.LogTrace("User: {userId} doesnt exist yet, creating them now...", userId);

                //If one doesn't exist, create one
                user = new(){
                    UserId = userId
                };
                database.Users.Add(user);
                await database.SaveChangesAsync();
            }

            logger.LogTrace("User: {userId} definitely exists", userId);
            return;
        }
    }
}