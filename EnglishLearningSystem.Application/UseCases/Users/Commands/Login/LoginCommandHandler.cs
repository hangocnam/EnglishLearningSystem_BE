using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using EnglishLearningSystem.Application.Common.Messaging;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.Login
{
    internal sealed class LoginCommandHandler(
        IUserRepository userRepo,
        IPasswordHasher passwordHasher,
        ITokenService tokenService
        ) : ICommandHandler<LoginCommand, LoginResponse>
    {
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetByEmailAsync(request.Email, cancellationToken);

            // Always verify (even when user is null) so response time is the same
            var isPasswordValid = passwordHasher.VerifyPassword(request.Password, user?.PasswordHash);
            
            if (user is null || !isPasswordValid)
            {
                return UserErrors.InvalidCredentials;
            }

            var token = tokenService.GenerateToken(user);

            return new LoginResponse(token, user.UserName, user.Role);
        }
    }
}
