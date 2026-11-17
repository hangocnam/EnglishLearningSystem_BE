using EnglishLearningSystem.Domain.Common;
using EnglishLearningSystem.Domain.Common.Utilities;
using EnglishLearningSystem.Domain.Constants;

namespace EnglishLearningSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; } = default!;
        public string? FullName { get; private set; }
        public string Email { get; private set; } = default!;
        public string? PhoneNumber { get; private set; }
        public string PasswordHash { get; private set; } = default!;
        public string Role { get; private set; } = default!;

        private User() { }

        public static User Create(string userName, string? fullName, string email, string passwordHash, string? phoneNumber)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                UserName = userName.Trim(),
                FullName = fullName?.Trim(),
                Email = TextUtilities.Normalize(email),
                PasswordHash = passwordHash,
                PhoneNumber = phoneNumber?.Trim(),
                Role = Roles.User
            };
        }
    }
}
