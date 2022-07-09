using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Config;
using Microsoft.IdentityModel.Tokens;

namespace Services.Token {

    public class JwtTokenFactory {

        private readonly CryptographySettings settings;

        public JwtTokenFactory(UltiminerSettings settings) {
            this.settings = settings.CryptographySettings;
        }

        public string CreateToken(string id) {

            JwtSecurityTokenHandler handler = new ();

            SecurityTokenDescriptor tokenDescriptor = new (){

                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, id)
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