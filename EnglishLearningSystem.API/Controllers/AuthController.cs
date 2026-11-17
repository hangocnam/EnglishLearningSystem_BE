using EnglishLearningSystem.Application.UseCases.Users.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningSystem.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(ISender sender) : ApiControllerBase(sender)
    {
        [AllowAnonymous]
        [HttpPost]
        public Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
            => SendAsync(command, cancellationToken);
    }
}
