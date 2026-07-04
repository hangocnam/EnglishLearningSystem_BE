using EnglishLearningSystem.Domain.Entities;

namespace EnglishLearningSystem.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
