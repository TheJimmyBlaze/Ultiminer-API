using Services.Token;

namespace Controllers.Authentication {

    public class AuthController {

        private readonly JwtTokenFactory tokenFactory;

        public AuthController(JwtTokenFactory tokenFactory) {
            this.tokenFactory = tokenFactory;
        }

        public async Task<IResult> PostAuthCode(string authCode) {

            //TODO: Exchange the code for a discord JWT.
            //TODO: Use the discord JWT to get bearer identity.

            string ultiminerToken = await Task.Run(() => tokenFactory.CreateToken());
            return Results.Ok(ultiminerToken);
        }
    }
}