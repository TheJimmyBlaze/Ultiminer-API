
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Users {

    public class UserManagement {

        private readonly ILogger logger;

        private readonly UltiminerContext database;

        public UserManagement(ILogger<UserManagement> logger,
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

        public async Task<User> GetUserForId(string userId) {

            logger.LogTrace("Getting user: {userId}...", userId);

            User? user = await database.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            
            //If we didn't get a user just throw, we don't really want to check a users nullability on every API call.
            if (user == null) {
                throw new Exception($"Unable to get user for userId: {userId}");
            }
            return user;
        }
    }
}