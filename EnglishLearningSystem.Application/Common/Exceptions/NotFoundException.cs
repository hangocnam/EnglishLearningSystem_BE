using EnglishLearningSystem.Application.Common.Errors;

namespace EnglishLearningSystem.Application.Common.Exceptions
{
    public class NotFoundException(string entityName, string field, object value) : ApplicationException(ErrorCodes.General.NotFound)
    {
        public string EntityName { get; } = entityName;

        public string Field { get; } = field;

        public object Value { get; } = value;
    }

    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(string field, object value)
            : base(typeof(T).Name, field, value)
        {
        }
    }
}
