using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Application.Common.Messaging;

namespace EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo
{
    public sealed record GetCurrentUserInfoQuery : IQuery<GetCurrentUserInfoResponse>;

    public sealed class GetCurrentUserInfoResponse
    {
        public Guid Id { get; init; }
        public string UserName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? PhoneNumber { get; init; }
        public DateTime CreatedAt { get; init; } = default!;
    }
}
