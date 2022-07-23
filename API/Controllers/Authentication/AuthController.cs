using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using System.Security.Claims;
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
        [AllowAnonymous]
        public async Task<IResult> PostDiscordAuthCode([FromBody] DiscordAuthCode code) {

            try {

                //Get discord bearer token
                DiscordToken discordToken = await discordAuthentication.ExchangeAuthCode(code.AuthCode);

                //Resolve discord user identity from bearer
                DiscordIdentity discordIdentity = await discordAuthentication.GetIdentityFromToken(discordToken);

                //Create Ultiminer token using discord identity data
                UltiminerToken ultiminerToken = await Task.Run(() => ultiminerAuthentication.CreateToken(discordIdentity));

                return Results.Ok(ultiminerToken);

            } catch (Exception ex) {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpGet("@Me")]
        public IResult GetMe() {

            try {

                IIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity is ClaimsIdentity token) {

                    string id = token.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    string name = token.FindFirst(ClaimTypes.Name)!.Value;

                    UltiminerIdentity response = new() {
                        Id = id,
                        Username = name
                    };
                    return Results.Ok(response);
                }
                throw new UnauthorizedAccessException();

            } catch (Exception) {
                return Results.Unauthorized();
            }
        }
    }
}