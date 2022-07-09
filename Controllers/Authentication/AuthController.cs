using Microsoft.AspNetCore.Mvc;
using Services.Token;

namespace Controllers.Authentication {

    [ApiController]
    public class AuthController : ControllerBase {

        private readonly JwtTokenFactory tokenFactory;

        public AuthController(JwtTokenFactory tokenFactory) {
            this.tokenFactory = tokenFactory;
        }

        [HttpPost("DiscordAuthCode")]
        public async Task<IResult> PostDiscordAuthCode([FromBody] string authCode) {

            //TODO: Exchange the code for a discord JWT.
            //TODO: Use the discord JWT to get bearer identity.

            string ultiminerToken = await Task.Run(() => tokenFactory.CreateToken(authCode));
            return Results.Ok(ultiminerToken);
        }
    }
}