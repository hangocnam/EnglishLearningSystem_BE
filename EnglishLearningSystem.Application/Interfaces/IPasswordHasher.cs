namespace EnglishLearningSystem.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string GeneratePasswordHashed(string password);

        bool VerifyPassword(string password, string passwordHashed);
    }
}
