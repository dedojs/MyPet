using Microsoft.IdentityModel.Tokens;
using MyPet.Domain.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyPet.Services.Token
{
    public class TokenGenerator
    {
        public string Generate(Tutor tutor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(tutor),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),
                    SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(1)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity AddClaims(Tutor tutor)
        {
            var claims = new ClaimsIdentity();

            var tutorClaims = new List<Claim>()
            {
                new Claim("TutorName", tutor.Nome),
                new Claim("TutorPets", tutor.Pets.ToList().ToString()),
                new Claim("TutorEmail", tutor.Email)
            };

            claims.AddClaims(tutorClaims);

            return claims;
        }
    }
}
