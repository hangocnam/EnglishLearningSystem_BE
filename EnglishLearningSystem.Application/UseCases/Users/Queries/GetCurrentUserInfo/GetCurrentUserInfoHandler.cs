using EnglishLearningSystem.Application.Common.Exceptions;
using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo
{
    public sealed class GetCurrentUserInfoHandler : IRequestHandler<GetCurrentUserInfoQuery, GetCurrentUserInfoResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;

        public GetCurrentUserInfoHandler(IUserRepository userRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public Task<GetCurrentUserInfoResponse> Handle(GetCurrentUserInfoQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserId is null)
            {
                throw new UnauthorizedException();
            }


        }
    }
}
