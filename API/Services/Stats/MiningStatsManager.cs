
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Stats {

    public class MiningStatsManager {

        private readonly ILogger logger;

        private readonly UltiminerContext database;

        public MiningStatsManager(ILogger<MiningStatsManager> logger,
            UltiminerContext database) {

            this.logger = logger;
            this.database = database;
        }

        public async Task<bool> CanMine(string userId) {

            logger.LogTrace("Determining if user: {userId}'s mining cooldown has elapsed...", userId);

            MiningStats? stats = await database.MiningStats
                .FirstOrDefaultAsync(stats => stats.UserId == userId);

            //If the user has no stats, they've never mined, so they can mine now
            if (stats == null) {
                return true;
            }

            //If they do have stats, check their next mine is equal to, or after now
            return stats.NextMine <= DateTime.UtcNow;
        }

        public async Task Mine(string userId, DateTime nextMine) {

            logger.LogTrace("Recording: 1 mine action against user: {userId}...", userId);

            MiningStats? stats = await database.MiningStats
                .FirstOrDefaultAsync(stats => stats.UserId == userId);

            DateTime lastMine = DateTime.UtcNow;
            if (stats == null) {

                //If no existing stats, make new ones
                stats = new (){
                    UserId = userId,
                    LastMine = lastMine,
                    NextMine = nextMine,
                    TotalMineActions = 1
                };
                database.MiningStats.Add(stats);
            } else {

                //If existing stats, update and increment
                stats.LastMine = lastMine;
                stats.NextMine = nextMine;
                stats.TotalMineActions ++;
            }

            //Save the stats
            await database.SaveChangesAsync();
            logger.LogTrace("User: {userId} has mined a total of: {total} times", userId, stats.TotalMineActions);
        }
    }
}