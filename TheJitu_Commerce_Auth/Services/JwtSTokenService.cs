using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheJitu_Commerce_Auth.Model;
using TheJitu_Commerce_Auth.Services.IService;
using TheJitu_Commerce_Auth.Utility;

namespace TheJitu_Commerce_Auth.Services
{
    public class JwtSTokenService : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtSTokenService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }
        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            //signing credentials
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //payload-data
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            //add role
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            //create token
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
