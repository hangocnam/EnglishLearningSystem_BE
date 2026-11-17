namespace EnglishLearningSystem.Domain.Common.Results;

public enum ErrorType { Validation, Unauthorized, Forbidden, NotFound, Conflict, BusinessRule, Unavailable, Unexpected}

public sealed record FieldError(string Field, string Code, IReadOnlyDictionary<string, object?>? Params = null);

public sealed record Error(
    string Code,
    ErrorType Type,
    IReadOnlyDictionary<string, object?>? Params = null,
    IReadOnlyList<FieldError>? FieldErrors = null)
{
    private static Error Create(string code, ErrorType type, (string Key, object? Value)[] args) =>
        new(code, type, args.Length == 0 ? null : args.ToDictionary(a => a.Key, a => a.Value));
    
    public static Error Validation(string code, params (string, object?)[] args) => Create(code, ErrorType.Validation, args);
    public static Error Unauthorized(string code, params (string, object?)[] args) => Create(code, ErrorType.Unauthorized, args);
    public static Error NotFound(string code, params (string, object?)[] args) => Create(code, ErrorType.NotFound, args);
    public static Error Conflict(string code, params (string, object?)[] args) => Create(code, ErrorType.Conflict, args);
    public static Error BusinessRule(string code, params (string, object?)[] args) => Create(code, ErrorType.BusinessRule, args);
    public static Error Forbidden(string code, params (string, object?)[] args) => Create(code, ErrorType.Forbidden, args);
    public static Error Unavailable(string code, params (string, object?)[] args) => Create(code, ErrorType.Unavailable, args);
    public static Error Unexpected(string code, params (string, object?)[] args) => Create(code, ErrorType.Unexpected, args);
}
