using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Config;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Services.Authentication {

    public class UltiminerAuthentication {

        private readonly CryptographySettings settings;

        public UltiminerAuthentication(UltiminerSettings settings) {
            this.settings = settings.Cryptography;
        }

        public string CreateToken(DiscordIdentity identity) {

            JwtSecurityTokenHandler handler = new ();

            //Create a new token containing useful identifying information
            SecurityTokenDescriptor tokenDescriptor = new (){

                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, identity.Id),
                    new Claim(ClaimTypes.Name, identity.Username),
                    new Claim(UltiminerClaims.DiscordDiscriminator, identity.Discriminator),
                    new Claim(UltiminerClaims.DiscordAvatarHash, identity.AvatarHash)
                }),
                Expires = DateTime.UtcNow.AddMinutes(settings.TokenMinsToLive),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(settings.GetSecret()),
                    SecurityAlgorithms.HmacSha256
                )
            };
            SecurityToken token = handler.CreateToken(tokenDescriptor);
            string tokenString = handler.WriteToken(token);

            return tokenString;
        }
    }
}