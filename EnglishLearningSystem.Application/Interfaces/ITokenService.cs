using EnglishLearningSystem.Domain.Entities;
using System.Security.Claims;

namespace EnglishLearningSystem.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);

        ClaimsPrincipal? ValidateToken(string token);
    }
}
