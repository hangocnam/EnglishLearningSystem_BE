using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Services;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using EnglishLearningSystem.Application.Common.Messaging;

namespace EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo
{
    internal sealed class GetCurrentUserInfoHandler(ICurrentUser currentUser, IUserQueryService userQueryService) : IQueryHandler<GetCurrentUserInfoQuery, GetCurrentUserInfoResponse>
    {
        public async Task<Result<GetCurrentUserInfoResponse>> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            Guid? userId = currentUser.UserId;
            
            if (userId is null) return AuthErrors.Unauthenticated;

            var userInfo = await userQueryService.GetCurrentUserInfoAsync(userId.Value, cancellationToken);

            if (userInfo is null) return UserErrors.NotFound(userId.Value);

            return userInfo;
        }
    }
}
