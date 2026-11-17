using EnglishLearningSystem.Application.Interfaces;

namespace EnglishLearningSystem.Infrastructure.Securities
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 11;

        // DummyHash
        private static readonly string DummyHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString(), WorkFactor);

        public string GeneratePasswordHashed(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        public bool VerifyPassword(string password, string? passwordHashed)
        {
            var isValid = BCrypt.Net.BCrypt.Verify(password, passwordHashed ?? DummyHash);
            return isValid && passwordHashed is not null;
        }
    }
}
