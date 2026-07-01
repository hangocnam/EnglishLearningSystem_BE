using EnglishLearningSystem.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.Common.Exceptions
{
    public class ConflictException : ApplicationException
    {
        public string EntityName { get; }
        public string Field { get; }
        public object Value { get; }

        public ConflictException(string entityName, string field, object value) : base(ErrorCodes.General.Conflict)
        {
            EntityName = entityName;
            Field = field;
            Value = value;
        }
    }

    public class ConfictException<T> : ConflictException
    {
        public ConfictException(string field, object value) : base(typeof(T).Name, field, value)
        {

        }
    }
}
