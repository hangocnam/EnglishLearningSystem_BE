using EnglishLearningSystem.API.Errors;
using EnglishLearningSystem.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningSystem.API.Controllers;

/// <summary>
/// Base controller: sends requests through MediatR and converts <see cref="Result"/> / <see cref="Result{T}"/> into HTTP responses.
/// Errors use the same mapping as GlobalExceptionHandler (<see cref="ErrorHttpMapping"/>).
/// </summary>
[ApiController]
public abstract class ApiControllerBase(ISender sender) : ControllerBase
{
    protected async Task<IActionResult> SendAsync<T>(IRequest<Result<T>> request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ErrorResult(result.Error!);
    }

    protected async Task<IActionResult> SendAsync(IRequest<Result> request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result.IsSuccess ? NoContent() : ErrorResult(result.Error!);
    }

    protected ObjectResult ErrorResult(Error error)
    {
        var problem = error.ToProblemDetails(HttpContext);
        return new ObjectResult(problem)
        {
            StatusCode = problem.Status,
            ContentTypes = { "application/problem+json" }
        };
    }
}
