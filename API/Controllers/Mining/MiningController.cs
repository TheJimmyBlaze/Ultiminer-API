
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Services.Loot;
using Models.Mining;
using Exceptions;

namespace Controller.Mining {

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

                MiningResult mined = await miner.Mine(userId, node.NodeId);
                return Results.Ok(mined); 
                
            } catch(TooManyRequestsException ex) {

                logger.LogDebug("Mining error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest("Mining action on cooldown, wait a few seconds");
            } catch(ArgumentException ex) {

                logger.LogDebug("Mining error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.NotFound($"{node.NodeId} is not a valid Resource Node");
            } catch(UnauthorizedAccessException ex) {
                
                logger.LogDebug("Mining error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"User is below the level required to mine {node.NodeId}");
            } catch(Exception ex) {

                logger.LogDebug("Mining error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"Something went wrong trying to mine: {node.NodeId}");
            }
        }
    }
}