using EnglishLearningSystem.Domain.Common.Results;

namespace EnglishLearningSystem.Application.Common.Exceptions;

public class AppException (Error error, Exception? inner = null) : Exception(error.Code, inner)
{
    public Error Error { get; } = error;
}
