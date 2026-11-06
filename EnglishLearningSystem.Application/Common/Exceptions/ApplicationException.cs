namespace EnglishLearningSystem.Application.Common.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public string ErrorCode { get; }

        protected ApplicationException(string errorCode) : base(nameof(ApplicationException)) => ErrorCode = errorCode;
    }
}
