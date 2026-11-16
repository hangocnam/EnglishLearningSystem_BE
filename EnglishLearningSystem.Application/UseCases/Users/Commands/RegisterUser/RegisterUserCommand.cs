using EnglishLearningSystem.Domain.Common.Results;
using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
            string Email,
            string Password,
            string UserName,
            string PhoneNumber

        ) : IRequest<Result<RegisterUserResponse>>;

    public record RegisterUserResponse(Guid UserId, string Email);
}
