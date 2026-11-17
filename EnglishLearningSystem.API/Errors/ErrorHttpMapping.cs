using System.Diagnostics;
using EnglishLearningSystem.Domain.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace EnglishLearningSystem.API.Errors;

/// <summary>
/// Mapping Error in Domain to HTTP response.
/// Shared by GlobalExceptionHandler (exceptions) and controllers (returned Result).
/// </summary>
public static class ErrorHttpMapping
{
    public static int ToStatusCode(this ErrorType type) => type switch
    {
        ErrorType.Validation   => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden    => StatusCodes.Status403Forbidden,
        ErrorType.NotFound     => StatusCodes.Status404NotFound,
        ErrorType.Conflict     => StatusCodes.Status409Conflict,
        ErrorType.BusinessRule => StatusCodes.Status422UnprocessableEntity,
        ErrorType.Unavailable  => StatusCodes.Status503ServiceUnavailable,
        _                      => StatusCodes.Status500InternalServerError
    };

    public static ProblemDetails ToProblemDetails(this Error error, HttpContext http)
    {
        var status = error.Type.ToStatusCode();
        var problem = new ProblemDetails
        {
            Status = status,
            Title = ReasonPhrases.GetReasonPhrase(status),
            Detail = error.Code, // TODO (IErrorLocalizer)
            Instance = http.Request.Path
        };

        problem.Extensions["errorCode"] = error.Code;
        problem.Extensions["traceId"] = Activity.Current?.Id ?? http.TraceIdentifier;

        if (error.Params is { Count: > 0 })
        {
            problem.Extensions["params"] = error.Params;
        }

        if (error.FieldErrors is { Count: > 0 })
        {
            // Return only error codes per field, not field Params (they may contain user input, e.g. passwords)
            problem.Extensions["errors"] = error.FieldErrors
                .GroupBy(f => f.Field)
                .ToDictionary(g => g.Key, g => g.Select(f => f.Code).ToArray());
        }

        return problem;
    }
}
