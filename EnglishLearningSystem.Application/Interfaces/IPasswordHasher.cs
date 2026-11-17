namespace EnglishLearningSystem.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string GeneratePasswordHashed(string password);

        /// <summary>
        /// Verifies a password. Pass <c>null</c> when the user is not found: the check still runs at the same cost
        /// (prevents email enumeration via response timing) and always returns <c>false</c>.
        /// </summary>
        bool VerifyPassword(string password, string? passwordHashed);
    }
}
