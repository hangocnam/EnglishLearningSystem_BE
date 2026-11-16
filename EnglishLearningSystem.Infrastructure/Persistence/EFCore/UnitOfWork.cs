using EnglishLearningSystem.Application.Interfaces;

namespace EnglishLearningSystem.Infrastructure.Persistence.EFCore
{
    public sealed class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
    }
}
