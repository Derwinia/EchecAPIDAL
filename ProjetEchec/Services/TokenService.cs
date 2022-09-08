using Microsoft.IdentityModel.Tokens;
using ProjetEchec.DTO;
using ProjetEchecDAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetEchec.Services
{
    public class TokenConfig
    {
        public string Signature { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
    }

    public class TokenService
    {
        private readonly TokenConfig _config;

        public TokenService(TokenConfig config)
        {
            _config = config;
        }

        public string CreateToken(JoueurDTO u)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                _config.Issuer, // issuer
                null, // audience
                CreateClaims(u), // claims <=> revendications (info user)
                DateTime.Now, // nfb <=> Not before 
                null, // exp <=> expired
                CreateCredentials() // Signature
            );


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        private SigningCredentials CreateCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Signature)),
                SecurityAlgorithms.HmacSha256
            );
        }

        private IEnumerable<Claim> CreateClaims(JoueurDTO u)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, u.Id.ToString());
            yield return new Claim(ClaimTypes.Name, u.Pseudo);
            yield return new Claim(ClaimTypes.Role, u.Droit.ToString());
            yield return new Claim(ClaimTypes.Gender, u.Genre.ToString());
            yield return new Claim(ClaimTypes.DateOfBirth, u.Birthday.ToString(),ClaimValueTypes.Date);
            yield return new Claim(ClaimTypes.PostalCode, u.Elo.ToString());

        }
    }
}
