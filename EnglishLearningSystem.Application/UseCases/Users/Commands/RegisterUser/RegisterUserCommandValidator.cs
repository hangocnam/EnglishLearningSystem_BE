using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8).WithMessage("Min 8 characters")
                .Matches("[A-Z]").WithMessage("It nhat 1 chu viet hoa")
                .Matches("[0-9]").WithMessage("It nhat co 1 so");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên là bắt buộc")
                .MaximumLength(100);
        }
    }
}
