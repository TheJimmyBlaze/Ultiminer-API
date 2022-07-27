
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Services.Loot;

namespace Controller.Loot {

    [ApiController]
    public class MiningController : ControllerBase {

        private readonly ILogger logger;

        private readonly LootTableIndex index;
        private readonly UltiminerAuthentication auth;

        public MiningController(ILogger<MiningController> logger, 
            LootTableIndex index,
            UltiminerAuthentication auth){

            this.logger = logger;

            this.index = index;
            this.auth = auth;
        }

        [HttpGet("Mine")]
        public IResult Mine() {

            IIdentity? token = HttpContext.User.Identity;
            string userId = auth.GetUserFromToken(token);
            logger.LogDebug("User: {userId} is mining...", userId);            
            
            return Results.Ok(index.GenerateLoot("Node.Stone"));
        }
    }
}