using System.Diagnostics;
using System.Text.Json;
using EnglishLearningSystem.API.Localization;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using JsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

namespace EnglishLearningSystem.API.Errors;

/// <summary>
/// Mapping Error in Domain to HTTP response.
/// Shared by GlobalExceptionHandler (exceptions) and controllers (returned Result).
/// </summary>
public static class ErrorHttpMapping
{
    public const string ErrorCodeKey = "errorCode";

    /// <summary>Key in "errors" for problems with the whole request body (malformed JSON, empty body).</summary>
    public const string BodyFieldKey = "body";

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

    public static string FallbackErrorCodeFor(int status) => status switch
    {
        StatusCodes.Status401Unauthorized         => AuthErrors.Unauthenticated.Code,
        StatusCodes.Status403Forbidden            => AuthErrors.Forbidden.Code,
        StatusCodes.Status404NotFound             => GeneralErrors.NotFound.Code,
        StatusCodes.Status405MethodNotAllowed     => GeneralErrors.MethodNotAllowed.Code,
        StatusCodes.Status415UnsupportedMediaType => GeneralErrors.UnsupportedMediaType.Code,
        >= StatusCodes.Status500InternalServerError => GeneralErrors.Unexpected.Code,
        _                                         => GeneralErrors.BadRequest.Code
    };

    public static ProblemDetails ToProblemDetails(this Error error, HttpContext http)
    {
        var localizer = http.RequestServices.GetRequiredService<IErrorLocalizer>();
        var status = error.Type.ToStatusCode();
        var problem = new ProblemDetails
        {
            Status = status,
            Title = ReasonPhrases.GetReasonPhrase(status)
        };

        problem.ApplyErrorCode(http, error.Code, error.Params);

        if (error.Params is { Count: > 0 })
        {
            problem.Extensions["params"] = error.Params;
        }

        if (error.FieldErrors is { Count: > 0 })
        {
            var namingPolicy = GetJsonNamingPolicy(http);

            // Return code + localized message per field, not the raw field Params.
            // Field names follow the JSON names the client sent ("email"), whatever produced the error.
            problem.Extensions["errors"] = error.FieldErrors
                .GroupBy(f => ToFieldKey(f.Field, namingPolicy))
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => new FieldErrorResponse(f.Code, localizer.Localize(f.Code, f.Params))).ToArray());
        }

        return problem;
    }

    /// <summary>Fills detail (localized), instance, errorCode and traceId: the fields every error response has.</summary>
    public static void ApplyErrorCode(this ProblemDetails problem, HttpContext http, string code,
        IReadOnlyDictionary<string, object?>? args = null)
    {
        var localizer = http.RequestServices.GetRequiredService<IErrorLocalizer>();

        problem.Detail = localizer.Localize(code, args);
        problem.Instance = http.Request.Path;
        problem.Extensions[ErrorCodeKey] = code;
        problem.Extensions["traceId"] = Activity.Current?.Id ?? http.TraceIdentifier;
    }

    /// <summary>
    /// "Email" (FluentValidation, C# property) and "$.email" (System.Text.Json path) → "email".
    /// "" / "$" (the whole body) → <see cref="BodyFieldKey"/>.
    /// </summary>
    private static string ToFieldKey(string field, JsonNamingPolicy? namingPolicy)
    {
        if (field is "" or "$")
        {
            return BodyFieldKey;
        }

        if (field.StartsWith("$.", StringComparison.Ordinal))
        {
            field = field[2..];
        }

        if (namingPolicy is null)
        {
            return field;
        }

        // Nested / collection paths: "Address.Street" → "address.street", "Items[0].Name" → "items[0].name"
        return string.Join('.', field.Split('.').Select(namingPolicy.ConvertName));
    }

    private static JsonNamingPolicy? GetJsonNamingPolicy(HttpContext http) =>
        http.RequestServices.GetService<IOptions<JsonOptions>>()?.Value.JsonSerializerOptions.PropertyNamingPolicy;
}
