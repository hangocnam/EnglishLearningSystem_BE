using EnglishLearningSystem.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.Common.Exceptions
{
    public class UnauthorizedException(string errorCode = ErrorCodes.Auth.InvalidCredentials) : ApplicationException(errorCode)
    {
    }
}
