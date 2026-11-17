namespace EnglishLearningSystem.Infrastructure.Securities;

/// <summary>
/// Claim names used in the JWT. Kept short (not mapped to ClaimTypes.*) because JwtBearer sets MapInboundClaims = false.
/// </summary>
public static class AppClaimTypes
{
    public const string Role = "role";
}
