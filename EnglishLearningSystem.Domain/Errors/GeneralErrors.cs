using EnglishLearningSystem.Domain.Common.Results;

namespace EnglishLearningSystem.Domain.Errors;

public static class GeneralErrors
{
    public static Error ValidationFailed(IReadOnlyList<FieldError> fields) => 
        new("General.ValidationFailed", ErrorType.Validation, FieldErrors: fields);

    public static readonly Error Conflict = new("General.Conflict", ErrorType.Conflict);
    public static readonly Error Unexpected = new("General.Unexpected", ErrorType.Unexpected);

}
