using EnglishLearningSystem.Application.Common.Exceptions;
using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.Login
{
    public class LoginCommandHandler(
        IUserRepository _userRepository,
        IPasswordHasher _passwordHasher,
        ITokenService _tokenService
        ) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            string passwordHash = _passwordHasher.GeneratePasswordHashed(request.Password);

            if (!passwordHash.Equals(user?.PasswordHash))
            {
                throw new UnauthorizedException();
            }

            var token = _tokenService.GenerateToken(user);

            return new LoginResponse(token, user.UserName!, user.Role!);
        }
    }
}
