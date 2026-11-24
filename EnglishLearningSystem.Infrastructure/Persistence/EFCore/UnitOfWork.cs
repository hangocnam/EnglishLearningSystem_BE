using EnglishLearningSystem.Application.Interfaces;

namespace EnglishLearningSystem.Infrastructure.Persistence.EFCore
{
    public sealed class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);
    }
}
