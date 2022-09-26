
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

        public async Task Mine(string userId, DateTime nextMine) {

            logger.LogTrace("Recording: 1 mine action against user: {userId}...", userId);

            MiningStats? stats = await database.MiningStats
                .Where(stats => stats.UserId == userId)
                .FirstOrDefaultAsync();

            DateTime lastMine = DateTime.Now;
            if (stats == null) {

                //If no existing stats, make new ones
                stats = new MiningStats(){
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