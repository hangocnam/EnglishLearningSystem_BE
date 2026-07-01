using EnglishLearningSystem.Application.Interfaces;

namespace EnglishLearningSystem.Infrastructure.Securities
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GeneratePasswordHashed(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
