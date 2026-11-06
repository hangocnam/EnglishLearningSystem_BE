using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.Login
{
    public record LoginCommand
    (
        string Email,
        string Password
    ) : IRequest<LoginResponse>;


    public record LoginResponse
        (
            string Token,
            string UserName,
            string Role
        );
}
