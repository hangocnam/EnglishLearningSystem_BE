using MediatR;

namespace EnglishLearningSystem.Application.UseCases.Users.Queries.GetCurrentUserInfo
{
    public sealed record GetCurrentUserInfoQuery : IRequest<GetCurrentUserInfoResponse>;

    public sealed class GetCurrentUserInfoResponse
    {
        public Guid Id { get; init; }
        public string UserName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? PhoneNumber { get; init; }
        public DateTime CreatedAt { get; init; } = default!;
    }
}
