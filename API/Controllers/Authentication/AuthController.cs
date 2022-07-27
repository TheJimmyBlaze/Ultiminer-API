using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Authentication;
using Models;
using Services.Users;

namespace Controllers.Authentication {

    [ApiController]
    public class AuthController : ControllerBase {

        private readonly ILogger logger;

        private readonly UltiminerAuthentication ultiminerAuthentication;
        private readonly DiscordAuthentication discordAuthentication;
        private readonly UserManagement userManagement;

        public AuthController(ILogger<AuthController> logger,
            UltiminerAuthentication ultiminerAuthentication, 
            DiscordAuthentication discordAuthentication, 
            UserManagement userManagement) {

            this.logger = logger;

            this.ultiminerAuthentication = ultiminerAuthentication;
            this.discordAuthentication = discordAuthentication;
            this.userManagement = userManagement;
        }

        [HttpPost("DiscordAuthCode")]
        [AllowAnonymous]
        public async Task<IResult> PostDiscordAuthCode([FromBody] DiscordAuthCode code) {

            logger.LogTrace("Attempting Discord login...");
            try {

                //Get discord bearer token
                DiscordToken discordToken = await discordAuthentication.ExchangeAuthCode(code.AuthCode);

                //Resolve discord user identity from bearer
                DiscordIdentity discordIdentity = await discordAuthentication.GetIdentityFromToken(discordToken);

                //Create Ultiminer token using discord identity data
                UltiminerToken ultiminerToken = await Task.Run(() => ultiminerAuthentication.CreateToken(discordIdentity));

                //Ensure a user account exists
                await userManagement.EnsureUserExits(discordIdentity.Id);

                return Results.Ok(ultiminerToken);

            } catch (Exception ex) {
                logger.LogDebug("Discord login error: {error}, {stackTrace}", ex.Message, ex.StackTrace);
                return Results.BadRequest("Could not login with Discord");
            }
        }
    }
}