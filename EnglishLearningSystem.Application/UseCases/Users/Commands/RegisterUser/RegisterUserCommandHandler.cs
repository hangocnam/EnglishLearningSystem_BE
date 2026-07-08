using EnglishLearningSystem.Application.Common.Exceptions;
using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Application.Services;
using EnglishLearningSystem.Domain.Entities;
using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(
        IUnitOfWork _unitOfWork,
        IUserRepository _userRepository,
        IUserQueryService _userQueryService,
        IPasswordHasher _passwordHasher
        ) : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userQueryService.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                throw new UnauthorizedException();
            }

            if (await _userQueryService.ExistsByNameAsync(request.Email, cancellationToken))
            {
                throw new UnauthorizedException();
            }

            var passwordHash = _passwordHasher.GeneratePasswordHashed(request.Password);

            var user = User.Create(request.FullName, request.Email, passwordHash,  request.PhoneNumber);

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterUserResponse(user.Id, user.Email);
        }
    }
}
