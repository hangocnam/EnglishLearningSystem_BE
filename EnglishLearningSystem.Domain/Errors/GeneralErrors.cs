using EnglishLearningSystem.Domain.Common.Results;

namespace EnglishLearningSystem.Domain.Errors;

public static class GeneralErrors
{
    public static Error ValidationFailed(IReadOnlyList<FieldError> fields) => 
        new("General.ValidationFailed", ErrorType.Validation, FieldErrors: fields);

    public static readonly Error Conflict = Error.Conflict("General.Conflict");
    public static readonly Error DatabaseUnavailable = Error.Unavailable("General.DatabaseUnavailable");
    public static readonly Error Unexpected = Error.Unexpected("General.Unexpected");
}
