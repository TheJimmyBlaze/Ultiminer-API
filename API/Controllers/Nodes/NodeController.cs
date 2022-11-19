
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Services.Nodes;
using Models.Nodes;

namespace Controllers.Nodes {

    [ApiController]
    public class NodeController : ControllerBase {

        public ILogger logger;

        public readonly UltiminerAuthentication auth;
        public readonly NodeManager nodes;

        public NodeController(ILogger<NodeController> logger,
            UltiminerAuthentication auth,
            NodeManager nodes) {
            
            this.logger = logger;

            this.auth = auth;
            this.nodes = nodes;
        }

        [HttpGet("SelectedNode")]
        public IResult GetSelectedNode() {

            IIdentity? token = HttpContext.User.Identity;
            string userId = auth.GetUserFromToken(token);
            logger.LogDebug("User: {userId} requests their selected node...", userId);

            try {

                //Get selected node for user
                string selectedNode = nodes.GetSelectedNode(userId);
                return Results.Ok(selectedNode);

            } catch (Exception ex) {

                logger.LogDebug("Node error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"Something went wrong trying to get the selected node for user: {userId}");
            }
        }

        [HttpPost("SelectedNode")]
        public async Task<IResult> SetSelectedNode([FromBody] SelectNode node) {

            IIdentity? token = HttpContext.User.Identity;
            string userId = auth.GetUserFromToken(token);
            logger.LogDebug("User: {userId} attempts to set their selected node to: {nodeId}", userId, node.NodeId);

            try {

                //Set the selected node for the user
                await nodes.SelectNode(userId, node.NodeId);
                return Results.Ok();

            } catch(ArgumentException ex) {

                logger.LogDebug("Node error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.NotFound($"{node.NodeId} is not a valid Resource Node");
            } catch(UnauthorizedAccessException ex) {
                
                logger.LogDebug("Node error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"User is below the level required to selected {node.NodeId}");
            } catch (Exception ex) {

                logger.LogDebug("Node error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"Something went wrong setting selected node: {node.NodeId} for user: {userId}");
            }
        }

        [HttpGet("UnlockedNodes")]
        public async Task<IResult> GetUnlockedNodes() {

            IIdentity? token = HttpContext.User.Identity;
            string userId = auth.GetUserFromToken(token);
            logger.LogDebug("User: {userId} requests all nodes they have unlocked...", userId);

            try {

                //Get all unlocked nodes and return
                UnlockedNodes unlocked = await nodes.GetUnlockedNodes(userId);
                return Results.Ok(unlocked);

            } catch (Exception ex) {

                logger.LogDebug("Node error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"Something went wrong trying to get unlocked nodes for user: {userId}");
            }
        }
    }
}