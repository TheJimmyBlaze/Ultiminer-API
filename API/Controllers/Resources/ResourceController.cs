
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Services.Resources;
using Models.Resources;

namespace Controllers.Resources {

    [ApiController]
    public class ResourceController : ControllerBase {

        private readonly ILogger logger;

        private readonly UltiminerAuthentication auth;
        private readonly ResourceManager resources;

        public ResourceController(ILogger<ResourceController> logger,
            UltiminerAuthentication auth,
            ResourceManager resources) {
            
            this.logger = logger;

            this.auth = auth;
            this.resources = resources;
        }

        [HttpGet("Resources")]
        public async Task<IResult> GetResources() {

            IIdentity? token = HttpContext.User.Identity;
            string userId = auth.GetUserFromToken(token);
            logger.LogDebug("User: {userId} requests their resource total...", userId);

            try {

                //Get all resources, and return them in a total resource object
                List<ResourceStack> total = await resources.GetAllResources(userId);
                ResourceTotal response = new () {
                    TotalResources = total
                };
                return Results.Ok(response);
            } catch (Exception ex) {

                logger.LogDebug("Resource error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"Something went wrong trying to get resources for user: {userId}");
            }
        }
    }
}