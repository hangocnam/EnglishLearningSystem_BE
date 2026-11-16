using EnglishLearningSystem.Domain.Common.Results;

namespace EnglishLearningSystem.Domain.Errors
{
    public static class UserErrors
    {
        public static Error NotFound(Guid id) => Error.NotFound("User.NotFound", ("id", id));
        public static Error EmailAlreadyExists(string? email) => Error.Conflict("User.EmailAlreadyExists", ("email", email));
        public static Error UserNameAlreadyExists(string? userName) => Error.Conflict("User.UserNameAlreadyExists", ("userName", userName));
        public static Error InvalidCredentials => Error.Unauthorized("User.InvalidCredentials");
    }
}