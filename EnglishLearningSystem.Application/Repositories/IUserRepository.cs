using EnglishLearningSystem.Domain.Entities;

namespace EnglishLearningSystem.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}
