using System.Diagnostics;
using EnglishLearningSystem.API.Localization;
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
        var localizer = http.RequestServices.GetRequiredService<IErrorLocalizer>();
        var status = error.Type.ToStatusCode();
        var problem = new ProblemDetails
        {
            Status = status,
            Title = ReasonPhrases.GetReasonPhrase(status),
            Detail = localizer.Localize(error.Code, error.Params),
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
            // Return code + localized message per field, not the raw field Params
            problem.Extensions["errors"] = error.FieldErrors
                .GroupBy(f => f.Field)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => new FieldErrorResponse(f.Code, localizer.Localize(f.Code, f.Params))).ToArray());
        }

        return problem;
    }
}
