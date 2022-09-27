
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Services.Loot;
using Models.Resources;

namespace Controller.Loot {

    [ApiController]
    public class MiningController : ControllerBase {

        private readonly ILogger logger;

        private readonly UltiminerAuthentication auth;
        private readonly LootMiner miner;

        public MiningController(ILogger<MiningController> logger, 
            UltiminerAuthentication auth,
            LootMiner miner){

            this.logger = logger;

            this.auth = auth;
            this.miner = miner;
        }

        [HttpPost("Mine")]
        public async Task<IResult> Mine([FromBody] ResourceNode node) {

            IIdentity? token = HttpContext.User.Identity;
            string userId = auth.GetUserFromToken(token);
            logger.LogDebug("User: {userId} is mining...", userId);    

            try{

                NewResources mined = await miner.Mine(userId, node.NodeId);
                return Results.Ok(mined); 

            } catch(ArgumentException ex) {

                logger.LogDebug("Mining error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.NotFound($"{node.NodeId} is not a valid Resource Node");
            }catch(Exception ex) {

                logger.LogDebug("Mining error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"Something went wrong trying to mine: {node.NodeId}");
            }
        }
    }
}