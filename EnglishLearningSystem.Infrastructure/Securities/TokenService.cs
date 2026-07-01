using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Domain.Entities;
using System.Security.Claims;

namespace EnglishLearningSystem.Infrastructure.Securities
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
