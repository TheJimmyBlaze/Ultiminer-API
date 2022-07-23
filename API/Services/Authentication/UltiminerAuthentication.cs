using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Config;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Services.Authentication {

    public class UltiminerAuthentication {

        private readonly CryptographySettings settings;

        private const string TOKEN_TYPE = "Bearer";

        public UltiminerAuthentication(UltiminerSettings settings) {
            this.settings = settings.Cryptography;
        }

        public UltiminerToken CreateToken(DiscordIdentity identity) {

            JwtSecurityTokenHandler handler = new ();

            //Save this, since we need to include it in the token DTO
            int secondsToLive = settings.SecondsToLive;

            //Create a new token containing useful identifying information
            SecurityTokenDescriptor tokenDescriptor = new (){

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
            UltiminerToken dto = new () { 
                AccessToken = tokenString,
                ExpiresIn = secondsToLive,
                TokenType = TOKEN_TYPE
            };

            return dto;
        }
    }
}