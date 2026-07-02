using EnglishLearningSystem.Application.Interfaces;

namespace EnglishLearningSystem.Infrastructure.Securities
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GeneratePasswordHashed(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHashed);
        }
    }
}
