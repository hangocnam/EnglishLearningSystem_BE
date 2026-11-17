using EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser;
using EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(ISender sender) : ApiControllerBase(sender)
    {
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
            => SendAsync(command, cancellationToken);

        [Route("Current")]
        [HttpGet]
        public Task<IActionResult> GetCurrentUserInfo(CancellationToken cancellationToken)
            => SendAsync(new GetCurrentUserInfoQuery(), cancellationToken);
    }
}
