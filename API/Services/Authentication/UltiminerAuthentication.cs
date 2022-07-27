
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Config;
using Microsoft.IdentityModel.Tokens;
using Models.Authentication;

namespace Services.Authentication {

    public class UltiminerAuthentication {

        private readonly ILogger logger;

        private readonly CryptographySettings settings;

        private const string TOKEN_TYPE = "Bearer";

        public UltiminerAuthentication(ILogger<UltiminerAuthentication> logger,
            UltiminerSettings settings) {

            this.logger = logger;

            this.settings = settings.Cryptography;
        }

        public UltiminerToken CreateToken(DiscordIdentity identity) {

            logger.LogTrace("Creating Ultiminer token...");

            JwtSecurityTokenHandler handler = new();

            //Save this, since we need to include it in the token DTO
            int secondsToLive = settings.SecondsToLive;

            //Create a new token containing useful identifying information
            SecurityTokenDescriptor tokenDescriptor = new(){

                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, identity.Id),
                    new Claim(ClaimTypes.Name, identity.Username),
                    new Claim(UltiminerClaims.DiscordDiscriminator, identity.Discriminator),
                    new Claim(UltiminerClaims.DiscordAvatarHash, identity.AvatarHash)
                }),
                Expires = DateTime.UtcNow.AddSeconds(secondsToLive),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(settings.GetSecret()),
                    SecurityAlgorithms.HmacSha256
                )
            };
            SecurityToken token = handler.CreateToken(tokenDescriptor);
            string tokenString = handler.WriteToken(token);

            //Create the Ultiminer Token DTO
            UltiminerToken dto = new() { 
                AccessToken = tokenString,
                ExpiresIn = secondsToLive,
                TokenType = TOKEN_TYPE
            };

            logger.LogTrace("Ultiminer token created");
            return dto;
        }

        public string GetUserFromToken(IIdentity? identity) {

            logger.LogTrace("Getting user from Ultiminer token...");

            if (identity is ClaimsIdentity claims) {

                Claim? userClaim = claims.FindFirst(ClaimTypes.NameIdentifier);

                if (userClaim == null) {

                    logger.LogDebug("User resolution error: No UserIdentifier claim present in token");
                    return string.Empty;
                }

                string userId = userClaim.Value;

                logger.LogTrace("Collected user from Ultiminer token");
                return userId;
            }

            logger.LogDebug("User resolution error: Identity was not a claims identity, how did this even happen?");
            return string.Empty;
        }
    }
}