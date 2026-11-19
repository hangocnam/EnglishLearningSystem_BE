namespace EnglishLearningSystem.API.Localization;

/// <summary>Translates a Domain error code into a message in the current request culture.</summary>
public interface IErrorLocalizer
{
    /// <param name="code">Error code, used as the resource key.</param>
    /// <param name="args">Values for named placeholders in the message, e.g. {MinLength}.</param>
    string Localize(string code, IReadOnlyDictionary<string, object?>? args = null);
}
