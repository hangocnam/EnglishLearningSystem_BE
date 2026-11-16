using EnglishLearningSystem.Application.Common.Exceptions;
using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Services;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo
{
    internal sealed class GetCurrentUserInfoHandler(ICurrentUser currentUser, IUserQueryService userQueryService) : IRequestHandler<GetCurrentUserInfoQuery, Result<GetCurrentUserInfoResponse>>
    {
        public async Task<Result<GetCurrentUserInfoResponse>> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            Guid? userId = currentUser.UserId;
            
            if (userId is null)  return UserErrors.InvalidCredentials;

            var userInfo = await userQueryService.GetCurrentUserInfoAsync(userId.Value, cancellationToken);

            if (userInfo is null) return UserErrors.InvalidCredentials;

            return userInfo;
        }
    }
}
