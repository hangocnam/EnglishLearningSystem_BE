namespace EnglishLearningSystem.Domain.Common.Utilities;

public static class TextUtilities
{
    public static string Normalize(string value) => value.Trim().ToLowerInvariant();
}
