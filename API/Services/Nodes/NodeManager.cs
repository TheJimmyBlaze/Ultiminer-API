
using Database.Models;
using Models.Nodes;
using Models.Experience;
using Services.Experience;

namespace Services.Nodes {

    public class NodeManager {

        private readonly ILogger logger;
        private readonly NodeIndex nodeIndex;
        private readonly ExperienceManager experienceManager;

        public NodeManager(ILogger<NodeManager> logger,
            NodeIndex nodeIndex,
            ExperienceManager experienceManager) {

            this.logger = logger;
            this.nodeIndex = nodeIndex;
            this.experienceManager = experienceManager;
        }
        
        public async Task<UnlockedNodes> GetUnlockedNodes(string userId) {

            logger.LogTrace("Getting unlocked nodes for user: {userId}", userId);

            //Get level of user
            ExperienceTotal userExperience = await experienceManager.GetExperience(userId);
            int level = userExperience.Level;

            //Get all unlocked nodes and the next node to be unlocked, from the index
            List<Node> unlockedNodes = nodeIndex.GetForLevel(level);
            Node nextUnlock = nodeIndex.GetNextForLevel(level);

            //Convert the unlockedNodes to userNodes
            List<UserNode> unlockedUserNodes = unlockedNodes    
                .Select(node => new UserNode() {
                    NodeId = node.NaturalId,
                    DisplayName = node.DisplayName,
                    LevelRequired = node.LevelRequired
                }).ToList();

            UserNode nextUserUnlock = new() {
                NodeId = nextUnlock.NaturalId,
                DisplayName = nextUnlock.DisplayName,
                LevelRequired = nextUnlock.LevelRequired
            };

            //Create the model and return
            UnlockedNodes unlocked = new() {
                Unlocked = unlockedUserNodes,
                NextUnlock = nextUserUnlock
            };

            return unlocked;
        }
    }
}