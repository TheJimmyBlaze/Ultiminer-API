
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