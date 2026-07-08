using EnglishLearningSystem.Application.UseCases.Users.Commands.RegisterUser;
using EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [Authorize]
        [Route("Current")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserInfo(CancellationToken cancellationToken)
        {
            var query = new GetCurrentUserInfoQuery();
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        //[Authorize]
        //[Route("GetListUser")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        //{
        //    var response = await _mediator.Send(cancellationToken);

        //    return Ok(response);
        //}
    }
}
