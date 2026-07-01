using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Domain.Exceptions;
using MediatR;
using EnglishLearningSystem.Domain.Entities;
using EnglishLearningSystem.Application.Interfaces;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(
        IUnitOfWork _unitOfWork,
        IUserRepository _userRepository,
        IPasswordHasher _passwordHasher
        ) : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //if (await userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            //    throw new ConflictException($"Email '{request.Email}' đã được sử dụng.");

            var passwordHash = _passwordHasher.GeneratePasswordHashed(request.Password);

            var user = User.Create(request.FullName, request.Email, passwordHash,  request.PhoneNumber);

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterUserResponse(user.Id, user.Email);
        }
    }
}
