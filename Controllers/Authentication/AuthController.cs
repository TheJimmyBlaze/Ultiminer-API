using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Models;

namespace Controllers.Authentication {

    [ApiController]
    public class AuthController : ControllerBase {

        private readonly UltiminerAuthentication ultiminerAuthentication;
        private readonly DiscordAuthentication discordAuthentication;

        public AuthController(UltiminerAuthentication ultiminerAuthentication, DiscordAuthentication discordAuthentication) {
            this.ultiminerAuthentication = ultiminerAuthentication;
            this.discordAuthentication = discordAuthentication;
        }

        [HttpPost("DiscordAuthCode")]
        public async Task<IResult> PostDiscordAuthCode([FromBody] string authCode) {

            //Get discord bearer token
            DiscordToken discordToken = await discordAuthentication.ExchangeAuthCode(authCode);

            //Resolve discord user identity from bearer
            DiscordIdentity discordIdentity = await discordAuthentication.GetIdentityFromToken(discordToken);

            //Create Ultiminer token using discord identity data
            string ultiminerToken = await Task.Run(() => ultiminerAuthentication.CreateToken(discordIdentity));
            
            return Results.Ok(ultiminerToken);
        }
    }
}