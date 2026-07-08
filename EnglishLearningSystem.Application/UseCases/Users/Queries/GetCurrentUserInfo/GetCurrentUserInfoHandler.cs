using EnglishLearningSystem.Application.Common.Exceptions;
using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Application.Services;
using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo
{
    public sealed class GetCurrentUserInfoHandler : IRequestHandler<GetCurrentUserInfoQuery, GetCurrentUserInfoResponse>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserQueryService _userQueryService;

        public GetCurrentUserInfoHandler(ICurrentUser currentUser, IUserQueryService userQueryService)
        {
            _currentUser = currentUser;
            _userQueryService = userQueryService;
        }

        public async Task<GetCurrentUserInfoResponse> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            Guid? userId = _currentUser.UserId ?? throw new UnauthorizedException();

            var userInfo = await _userQueryService.GetCurrentUserInfoAsync(userId.Value, cancellationToken);

            return userInfo ?? throw new UnauthorizedException();
        }
    }
}
