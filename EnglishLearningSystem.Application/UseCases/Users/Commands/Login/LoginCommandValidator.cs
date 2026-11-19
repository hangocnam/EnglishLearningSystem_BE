using EnglishLearningSystem.Domain.Errors;
using FluentValidation;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithErrorCode(ValidationErrors.Required)
                .EmailAddress().WithErrorCode(ValidationErrors.InvalidEmail);

            RuleFor(x => x.Password)
                .NotEmpty().WithErrorCode(ValidationErrors.Required);
        }
    }
}
