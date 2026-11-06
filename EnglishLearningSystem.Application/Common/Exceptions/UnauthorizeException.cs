using EnglishLearningSystem.Application.Common.Errors;

namespace EnglishLearningSystem.Application.Common.Exceptions
{
    public class UnauthorizedException(string errorCode = ErrorCodes.Auth.InvalidCredentials) : ApplicationException(errorCode)
    {
    }
}
