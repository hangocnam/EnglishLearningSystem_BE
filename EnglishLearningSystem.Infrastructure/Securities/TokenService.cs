using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnglishLearningSystem.Infrastructure.Securities
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSetting;

        public TokenService(IOptions<JwtSettings> option)
        {
            _jwtSetting = option.Value;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSetting.SecretKey);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Name, user.UserName),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var targetAlgorithm = _jwtSetting.Algorithm.ToUpper() switch
            {
                "SHA256" => SecurityAlgorithms.HmacSha256Signature,
                _ => SecurityAlgorithms.HmacSha256Signature
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
                Issuer = _jwtSetting.Issuer,
                Audience = _jwtSetting.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    targetAlgorithm)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
