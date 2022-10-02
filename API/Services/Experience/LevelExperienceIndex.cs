
using Models.Mining;

namespace Services.Experience {

    public class LevelExperienceIndex {

        private readonly ILogger logger;
        
        public const int CURRENT_MAXIMUM_LEVEL = 50;
        private const int FIRST_LEVEL_EXP = 1000;
        private const double PERCENT_INCREASE_PER_LEVEL = 1.25;

        //Key: Level; Value: Experience required
        private readonly Dictionary<int, int> index = new();

        public LevelExperienceIndex(ILogger<LevelExperienceIndex> logger) {

            this.logger = logger;

            BuildIndex();
        }

        public int Get(int level) {
            return index[level];
        }

        private void BuildIndex() {
            
            logger.LogInformation("Building Level Experience Indexes...");

            //Clear existing index
            index.Clear();

            //Iterate from 1 to the current max, adding an index each time, increase by the percent increase constant value
            int nextLevelExp = FIRST_LEVEL_EXP;
            for(int level = 1; level <= CURRENT_MAXIMUM_LEVEL; level++) {

                index.Add(level, nextLevelExp);
                nextLevelExp = (int)(nextLevelExp * PERCENT_INCREASE_PER_LEVEL);
            }
        }
    }
}