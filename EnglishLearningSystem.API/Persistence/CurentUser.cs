using EnglishLearningSystem.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EnglishLearningSystem.API.Persistence
{
    public class CurentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirst(JwtRegisteredClaimNames.Sub)?
                    .Value;

                return Guid.TryParse(value, out var id) ? id : null;
            }
        }

        public string? UserName =>
             _httpContextAccessor.HttpContext?
                .User
                .FindFirst(JwtRegisteredClaimNames.Name)?
                .Value;

        public string? Email =>
             _httpContextAccessor.HttpContext?
                .User
                .FindFirst(JwtRegisteredClaimNames.Email)?
                .Value;

        public IReadOnlyCollection<string> Role =>
            _httpContextAccessor.HttpContext?
                .User
                .FindAll(ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList()
            ?? [];
    }
}
