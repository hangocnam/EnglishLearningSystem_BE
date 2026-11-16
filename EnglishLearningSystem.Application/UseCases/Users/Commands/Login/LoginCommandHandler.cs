using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Application.Services;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.Login
{
    internal sealed class LoginCommandHandler(
        IUserRepository userRepo,
        IPasswordHasher passwordHasher,
        ITokenService tokenService
        ) : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private const string DummyHash = "";
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetByEmailAsync(request.Email, cancellationToken);

            var isPasswordValid = passwordHasher.VerifyPassword(
                request.Password,
                user?.PasswordHash ?? DummyHash);
            
            if (user is null || !isPasswordValid)
            {
                return UserErrors.InvalidCredentials;
            }

            var token = tokenService.GenerateToken(user);

            return new LoginResponse(token, user.UserName, user.Role);
        }
    }
}
