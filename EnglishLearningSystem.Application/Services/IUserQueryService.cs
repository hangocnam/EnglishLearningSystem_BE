using EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo;

namespace EnglishLearningSystem.Application.Services
{
    public interface IUserQueryService
    {
        Task<GetCurrentUserInfoResponse?> GetCurrentUserInfoAsync(Guid userId, CancellationToken cancellationToken);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ExistsByNameAsync(string userName, CancellationToken cancellationToken);
    }
}
