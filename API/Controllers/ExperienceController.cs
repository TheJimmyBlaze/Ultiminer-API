
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Services.Experience;
using Models.Experience;

namespace Controllers {

    [ApiController]
    public class ExperienceController : ControllerBase {

        private readonly ILogger logger;

        private readonly UltiminerAuthentication auth;
        private readonly ExperienceManager exp;

        public ExperienceController(ILogger<ExperienceController> logger,
            UltiminerAuthentication auth,
            ExperienceManager exp) {

            this.logger = logger;

            this.auth = auth;
            this.exp = exp;
        }

        [HttpGet("Experience")]
        public async Task<IResult> GetExperience() {

            IIdentity? token = HttpContext.User.Identity;
            string userId = auth.GetUserFromToken(token);
            logger.LogDebug("User: {userId} requests their experience total...", userId);

            try {

                ExperienceTotal experience = await exp.GetExperience(userId);
                return Results.Ok(experience);
            } catch (Exception ex) {
                
                logger.LogDebug("Experience error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest($"Something went wrong trying to get experience for user: {userId}");
            }
        }
    }
}