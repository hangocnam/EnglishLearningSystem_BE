using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.Common.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public string ErrorCode { get; }

        protected ApplicationException(string errorCode) : base(nameof(ApplicationException)) => ErrorCode = errorCode;
    }
}
