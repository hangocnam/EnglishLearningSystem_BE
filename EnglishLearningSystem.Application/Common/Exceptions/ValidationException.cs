namespace EnglishLearningSystem.Application.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base(nameof(ValidationException))
        {
            Errors = errors.AsReadOnly();
        }

        public ValidationException(string field, string message): base(nameof(ValidationException))
        {
            Errors = new Dictionary<string, string[]>
            {
                { field, new[] { message } }
            }.AsReadOnly();
        }
    }
}
