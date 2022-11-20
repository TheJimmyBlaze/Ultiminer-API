

using Database;
using Database.Models;
using Data = Database.Data;
using Models.Nodes;
using Models.Experience;
using Services.Experience;

namespace Services.Nodes {

    public class NodeManager {

        private readonly ILogger logger;

        private readonly NodeIndex nodeIndex;
        private readonly ExperienceManager experienceManager;

        private readonly UltiminerContext database;

        public NodeManager(ILogger<NodeManager> logger,
            NodeIndex nodeIndex,
            ExperienceManager experienceManager,
            UltiminerContext database) {

            this.logger = logger;

            this.nodeIndex = nodeIndex;
            this.experienceManager = experienceManager;

            this.database = database;
        }

        public string GetSelectedNode(string userId) {

            logger.LogTrace("Getting selected node for user: {userId}", userId);

            //If a user hasn't selected a node yet, give them stone
            //Don't set this default in the DB unless the user has actually selected it
            const string DEFAULT_NODE = Data.Nodes.STONE;
            string selectedNode = DEFAULT_NODE;

            UserNode? userNode = database.UserNodes.FirstOrDefault(userNode => userNode.UserId == userId);
            if (userNode != null) {
                //If the userNode is not null, set it to be the selected node
                selectedNode = userNode.SelectedNodeId;
            }

            return selectedNode;
        }
        
        public async Task SelectNode(string userId, string nodeId) {

            logger.LogTrace("Setting selected node to: {nodeId} for user: {userId}", nodeId, userId);

            //Verify the nodeId resolves to a node
            Node? node = nodeIndex.Get(nodeId);
            if (node == null) {
                throw new ArgumentException($"Node: {nodeId} does not exist");
            }

            //Check if the players level is greater than or equal to the required node level
            ExperienceTotal userExperience = await experienceManager.GetExperience(userId);
            if (userExperience.Level < node.LevelRequired) {
                throw new UnauthorizedAccessException($"User: {userId} is below the required level ({node.LevelRequired}) to mine {nodeId}");
            }

            UserNode? userNode = database.UserNodes.FirstOrDefault(userNode => userNode.UserId == userId);
            if (userNode == null) {

                userNode = new() {
                    UserId = userId,
                    SelectedNodeId = nodeId
                };
                await database.UserNodes.AddAsync(userNode);
                
            } else {
                userNode.SelectedNodeId = nodeId;
            }

            await database.SaveChangesAsync();
        }

        public async Task<UnlockedNodes> GetUnlockedNodes(string userId) {

            logger.LogTrace("Getting unlocked nodes for user: {userId}", userId);

            //Get level of user
            ExperienceTotal userExperience = await experienceManager.GetExperience(userId);
            int level = userExperience.Level;

            //Get all unlocked nodes and the next node to be unlocked, from the index
            List<Node> unlockedNodes = nodeIndex.GetForLevel(level);
            Node? nextUnlock = nodeIndex.GetNextForLevel(level);

            //Convert the unlockedNodes to displayNode
            List<DisplayNode> unlockedDisplayNodes = unlockedNodes    
                .Select(node => new DisplayNode() {
                    NodeId = node.NaturalId,
                    DisplayName = node.DisplayName,
                    LevelRequired = node.LevelRequired
                }).ToList();

            
            DisplayNode? nextUserUnlock = null;
            if (nextUnlock != null) {
                    nextUserUnlock = new() {
                    NodeId = nextUnlock.NaturalId,
                    DisplayName = nextUnlock.DisplayName,
                    LevelRequired = nextUnlock.LevelRequired
                };
            }

            //Create the model and return
            UnlockedNodes unlocked = new() {
                Unlocked = unlockedDisplayNodes,
                NextUnlock = nextUserUnlock
            };

            return unlocked;
        }
    }
}