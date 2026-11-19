using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using FluentValidation;
using MediatR;

namespace EnglishLearningSystem.Application.Common.Behaviors
{
    internal sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : IResultFactory<TResponse>
    {
        // FluentValidation placeholders that carry user input or field names: never pass them on
        private static readonly HashSet<string> ExcludedParams = ["PropertyValue", "PropertyName", "PropertyPath"];

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!validators.Any())
            {
                return await next(ct);
            }

            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(request, ct)));

            var fieldErrors = results.SelectMany(r => r.Errors)
                .Select(f => new FieldError(f.PropertyName, f.ErrorCode, ToParams(f.FormattedMessagePlaceholderValues)))
                .ToList();

            return fieldErrors.Count == 0 ? await next(ct) : TResponse.Failure(GeneralErrors.ValidationFailed(fieldErrors));
        }

        private static IReadOnlyDictionary<string, object?>? ToParams(Dictionary<string, object>? placeholders)
        {
            var args = placeholders?
                .Where(p => !ExcludedParams.Contains(p.Key))
                .ToDictionary(p => p.Key, p => (object?)p.Value);

            return args is { Count: > 0 } ? args : null;
        }
    }
}
