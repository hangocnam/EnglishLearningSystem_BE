using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;

namespace EnglishLearningSystem.API.Localization;

internal sealed partial class ErrorLocalizer(
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<ErrorLocalizer> logger) : IErrorLocalizer
{
    public string Localize(string code, IReadOnlyDictionary<string, object?>? args = null)
    {
        var template = localizer[code];
        if (template.ResourceNotFound)
        {
            logger.LogWarning("Missing error message for code {ErrorCode} (culture {Culture})",
                code, CultureInfo.CurrentUICulture.Name);
            return code;
        }

        if (args is not { Count: > 0 })
        {
            return template.Value;
        }

        // Named placeholders ({MinLength}); unknown ones are left as-is
        return PlaceholderRegex().Replace(template.Value, m =>
            args.TryGetValue(m.Groups[1].Value, out var value)
                ? Convert.ToString(value, CultureInfo.CurrentCulture) ?? string.Empty
                : m.Value);
    }

    [GeneratedRegex(@"\{(\w+)\}")]
    private static partial Regex PlaceholderRegex();
}
