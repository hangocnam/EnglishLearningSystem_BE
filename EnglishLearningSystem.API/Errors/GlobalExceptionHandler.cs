using EnglishLearningSystem.Application.Common.Exceptions;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using EnglishLearningSystem.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace EnglishLearningSystem.API.Errors;

/// <summary>
/// Converts every unhandled exception into a Domain <see cref="Error"/>,
/// Return Code and details (<see cref="ErrorHttpMapping"/>).
/// </summary>
internal sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    IHostEnvironment environment,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private const int ClientClosedRequest = 499;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // Client cancel request
        if (exception is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested)
        {
            httpContext.Response.StatusCode = ClientClosedRequest;
            return true;
        }

        var (error, logLevel) = Map(exception);
        logger.Log(logLevel, exception, "Request {Method} {Path} failed with {ErrorCode}",
            httpContext.Request.Method, httpContext.Request.Path, error.Code);

        var problem = error.ToProblemDetails(httpContext);
        if (error.Type == ErrorType.Unexpected && environment.IsDevelopment())
        {
            problem.Detail = exception.ToString();
        }

        httpContext.Response.StatusCode = problem.Status!.Value;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problem,
            Exception = exception
        });
    }

    private static (Error Error, LogLevel Level) Map(Exception exception) => exception switch
    {
        DomainException ex => (ex.Error, LogLevel.Warning),
        AppException ex => (ex.Error, LogLevel.Warning),

        DbUpdateConcurrencyException => (GeneralErrors.Conflict, LogLevel.Warning),
        DbUpdateException { InnerException: PostgresException { SqlState: PostgresErrorCodes.UniqueViolation } }
            => (GeneralErrors.Conflict, LogLevel.Warning), // TODO unique index: map ConstraintName

        RetryLimitExceededException or NpgsqlException { IsTransient: true } or TimeoutException
            => (GeneralErrors.DatabaseUnavailable, LogLevel.Error),

        _ => (GeneralErrors.Unexpected, LogLevel.Error)
    };
}
