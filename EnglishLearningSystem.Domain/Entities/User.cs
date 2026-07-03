using EnglishLearningSystem.Domain.Common;

namespace EnglishLearningSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string? PhoneNumber { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public string? Role { get; private set; } = default!;

        private User() { }

        public static User Create (string userName, string email, string passwordHash, string phoneNumber)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email,
                PasswordHash = passwordHash,
                PhoneNumber = phoneNumber
            };
        }
    }
}
