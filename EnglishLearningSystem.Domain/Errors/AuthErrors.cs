using EnglishLearningSystem.Domain.Common.Results;

namespace EnglishLearningSystem.Domain.Errors;

public static class AuthErrors
{
    /// <summary>The request has no valid identity (missing token, or the token has no user id).</summary>
    public static readonly Error Unauthenticated = Error.Unauthorized("Auth.Unauthenticated");

    /// <summary>The token has expired.</summary>
    public static readonly Error TokenExpired = Error.Unauthorized("Auth.TokenExpired");
}
