using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Application.Common.Messaging;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.Login
{
    public record LoginCommand
    (
        string Email,
        string Password
    ) : ICommand<LoginResponse>;


    public record LoginResponse
        (
            string Token,
            string UserName,
            string Role
        );
}
