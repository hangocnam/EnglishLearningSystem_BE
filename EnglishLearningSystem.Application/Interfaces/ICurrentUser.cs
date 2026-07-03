namespace EnglishLearningSystem.Application.Interfaces
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
        string? UserName { get; }
        string? Email { get; }
        IReadOnlyCollection<string> Role { get; }
    }
}
