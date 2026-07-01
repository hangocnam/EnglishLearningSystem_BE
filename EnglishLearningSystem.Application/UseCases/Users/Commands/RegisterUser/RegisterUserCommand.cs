using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
            string Email,
            string Password,
            string FullName,
            string PhoneNumber
    
        ) : IRequest<RegisterUserResponse>;

    public record RegisterUserResponse(Guid UserId, string Email);
}
