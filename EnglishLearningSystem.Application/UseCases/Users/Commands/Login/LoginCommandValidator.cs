using FluentValidation;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email invalid format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");
        }
    }
}
