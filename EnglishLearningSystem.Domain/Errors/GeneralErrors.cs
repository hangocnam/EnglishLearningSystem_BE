using EnglishLearningSystem.Domain.Common.Results;

namespace EnglishLearningSystem.Domain.Errors;

public static class GeneralErrors
{
    public static Error ValidationFailed(IReadOnlyList<FieldError> fields) => 
        new("General.ValidationFailed", ErrorType.Validation, FieldErrors: fields);

    public static readonly Error Conflict = Error.Conflict("General.Conflict");
    public static readonly Error NotFound = Error.NotFound("General.NotFound");

    // Request-level errors produced by the framework (malformed request, unknown method, wrong Content-Type).
    // The HTTP status comes from the framework, so the ErrorType here is only informative.
    public static readonly Error BadRequest = Error.Validation("General.BadRequest");
    public static readonly Error MethodNotAllowed = Error.Validation("General.MethodNotAllowed");
    public static readonly Error UnsupportedMediaType = Error.Validation("General.UnsupportedMediaType");

    public static readonly Error DatabaseUnavailable = Error.Unavailable("General.DatabaseUnavailable");
    public static readonly Error Unexpected = Error.Unexpected("General.Unexpected");
}
