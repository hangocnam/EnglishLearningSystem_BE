using EnglishLearningSystem.Domain.Errors;
using FluentValidation;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithErrorCode(ValidationErrors.Required)
                .EmailAddress().WithErrorCode(ValidationErrors.InvalidEmail);

            RuleFor(x => x.Password)
                .NotEmpty().WithErrorCode(ValidationErrors.Required)
                .MinimumLength(8).WithErrorCode(ValidationErrors.MinLength)
                .Matches("[A-Z]").WithErrorCode(ValidationErrors.MustContainUppercase)
                .Matches("[0-9]").WithErrorCode(ValidationErrors.MustContainDigit);

            RuleFor(x => x.UserName)
                .NotEmpty().WithErrorCode(ValidationErrors.Required)
                .MinimumLength(5).WithErrorCode(ValidationErrors.MinLength)
                .MaximumLength(15).WithErrorCode(ValidationErrors.MaxLength);

            RuleFor(x => x.FullName)
                .MaximumLength(50).WithErrorCode(ValidationErrors.MaxLength);

            RuleFor(x => x.PhoneNumber)
                .Length(12).WithErrorCode(ValidationErrors.ExactLength);
        }
    }
}
