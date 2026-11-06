using EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo;
using EnglishLearningSystem.Domain.Entities;

namespace EnglishLearningSystem.Application.Services
{
    public interface IUserQueryService
    {
        Task<GetCurrentUserInfoResponse?> GetCurrentUserInfoAsync(Guid userId, CancellationToken cancellationToken);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ExistsByNameAsync(string userName, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
