using EnglishLearningSystem.Domain.Common.Results;

namespace EnglishLearningSystem.Domain.Exceptions
{
    public class DomainException(Error error) : Exception (error.Code)
    {
        public Error Error { get; } = error;
    }
}
