using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Application.Services;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Entities;
using EnglishLearningSystem.Domain.Errors;
using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    internal sealed class RegisterUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IUserQueryService userQueryService,
        IPasswordHasher passwordHasher
        ) : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
    {
        public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isExistsEmail = await userQueryService.ExistsByEmailAsync(request.Email, cancellationToken);
            if (isExistsEmail) return UserErrors.EmailAlreadyExists(request.Email);

            var isExistsName = await userQueryService.ExistsByNameAsync(request.UserName, cancellationToken);
            if (isExistsName) return UserErrors.UserNameAlreadyExists(request.UserName);

            var passwordHash = passwordHasher.GeneratePasswordHashed(request.Password);
            var user = User.Create(request.UserName, request.FullName, request.Email, passwordHash, request.PhoneNumber);

            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterUserResponse(user.Id, user.Email);
        }
    }
}
