
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Users {

    public class UserManagement {

        private readonly UltiminerContext database;

        public UserManagement(UltiminerContext database) {
            this.database = database;
        }

        public async Task EnsureUserExits(string userId) {

            //Get a user if one exists
            User? user = await database.Users.SingleOrDefaultAsync(user => user.UserId == userId);
            if (user == null) {
                
                //If one doesn't exist, create one
                user = new(){
                    UserId = userId
                };
                database.Users.Add(user);
                await database.SaveChangesAsync();
            }

            return;
        }
    }
}