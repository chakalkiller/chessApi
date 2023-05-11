using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChessApi.Helpers
{
    public class JwtHelper
    {
        //acces aux valeurs  du " appsetting.json"
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(PlayerModel playerModel)
        {
            //generation de la signature du token

            SecurityKey securitytKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
                );

            SigningCredentials credentials = new SigningCredentials(securitytKey, SecurityAlgorithms.HmacSha256);

            // info qui seront contenu ds le jwt
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, playerModel.PlayerId.ToString()),
                new Claim(ClaimTypes.Email, playerModel.Email)
            };

            //creation du token jwt
            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            //envoyer le token sous la forme d une chaine jwt
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
