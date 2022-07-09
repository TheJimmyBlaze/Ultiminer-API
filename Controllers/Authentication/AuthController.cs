using Microsoft.AspNetCore.Mvc;
using Services.Token;

namespace Controllers.Authentication {

    [ApiController]
    public class AuthController : ControllerBase {

        private readonly UltiminerToken tokenFactory;
        private readonly DiscordToken tokenExchange;

        public AuthController(UltiminerToken tokenFactory, DiscordToken tokenExchange) {
            this.tokenFactory = tokenFactory;
            this.tokenExchange = tokenExchange;
        }

        [HttpPost("DiscordAuthCode")]
        public async Task<IResult> PostDiscordAuthCode([FromBody] string authCode) {

            //TODO: Exchange the code for a discord JWT.
            //TODO: Use the discord JWT to get bearer identity.

            string nothingString = await tokenExchange.ExchangeAuthCode(authCode);

            string ultiminerToken = await Task.Run(() => tokenFactory.CreateToken(authCode));
            return Results.Ok(ultiminerToken);
        }
    }
}