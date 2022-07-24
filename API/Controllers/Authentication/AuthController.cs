using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Authentication;
using Models;
using Services.Users;

namespace Controllers.Authentication {

    [ApiController]
    public class AuthController : ControllerBase {

        private readonly UltiminerAuthentication ultiminerAuthentication;
        private readonly DiscordAuthentication discordAuthentication;
        private readonly UserManagement userManagement;

        public AuthController(UltiminerAuthentication ultiminerAuthentication, 
            DiscordAuthentication discordAuthentication, 
            UserManagement userManagement) {

            this.ultiminerAuthentication = ultiminerAuthentication;
            this.discordAuthentication = discordAuthentication;
            this.userManagement = userManagement;
        }

        [HttpPost("DiscordAuthCode")]
        [AllowAnonymous]
        public async Task<IResult> PostDiscordAuthCode([FromBody] DiscordAuthCode code) {

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
                return Results.BadRequest(ex.Message);
            }
        }
    }
}