using EnglishLearningSystem.Application.Services;
using EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo;
using EnglishLearningSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningSystem.Infrastructure.Persistence.EFCore.Services
{
    public class UserQueryService : IUserQueryService
    {
        private readonly AppDbContext _context;

        public UserQueryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            return await _context.Users
                .AnyAsync(x => x.Email.ToLower() == normalizedEmail, cancellationToken);
        }

        public async Task<GetCurrentUserInfoResponse?> GetCurrentUserInfoAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(x => x.Id == userId)
                .Select(u => new GetCurrentUserInfoResponse()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    CreatedAt = u.CreatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string userName, CancellationToken cancellationToken)
        {
            var normalizedName = userName.Trim().ToLowerInvariant();
            return await _context.Users
                .AnyAsync(x => x.UserName.ToLower() == normalizedName, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.ToLower() == normalizedEmail, cancellationToken);
        }
    }
}
