using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Domain.Common.Utilities;
using EnglishLearningSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningSystem.Infrastructure.Persistence.EFCore.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var normalizedEmail = TextUtilities.Normalize(email);
            return await context.Users.FirstOrDefaultAsync(x => x.Email == normalizedEmail, ct);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await context.Users.AddAsync(user, ct);
        }
    }
}
