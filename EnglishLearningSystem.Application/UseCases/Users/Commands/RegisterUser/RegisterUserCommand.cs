using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Application.Common.Messaging;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
            string Email,
            string Password,
            string UserName,
            string? FullName,
            string? PhoneNumber

        ) : ICommand<RegisterUserResponse>;

    public record RegisterUserResponse(Guid UserId, string Email);
}
