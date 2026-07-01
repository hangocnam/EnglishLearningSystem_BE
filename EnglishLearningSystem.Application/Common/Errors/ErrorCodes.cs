using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishLearningSystem.Application.Common.Errors
{
    public class ErrorCodes
    {
        public static class Auth
        {
            public const string InvalidCredentials = "AUTH_001";
            public const string TokenExpired = "AUTH_002";
            public const string TokenInvalid = "AUTH_003";
        }

        public static class User
        {
            public const string NotFound = "USER_001";
            public const string EmailAlreadyExists = "USER_002";
            public const string UsernameNotFound = "USER_003";
        }

        public static class General
        {
            public const string NotFound = "GEN_001";
            public const string Conflict = "GEN_002";
            public const string Forbidden = "GEN_003";
            public const string ValidationFailed = "GEN_004";
            public const string InternalError = "GEN_005";
        }
    }
}
