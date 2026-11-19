namespace EnglishLearningSystem.API.Errors;

/// <summary>One validation error of a field in the ProblemDetails "errors" extension.</summary>
public sealed record FieldErrorResponse(string Code, string Message);
