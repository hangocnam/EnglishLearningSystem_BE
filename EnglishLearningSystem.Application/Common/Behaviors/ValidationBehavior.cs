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

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!validators.Any())
            {
                return await next(ct);
            }

            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(request, ct)));

            var fieldErrors = results.SelectMany(r => r.Errors)
                .Select(f => new FieldError(f.PropertyName, f.ErrorCode, 
                    f.FormattedMessagePlaceholderValues?.ToDictionary(p => p.Key, p => (object?)p.Value)))
                .ToList();
            

            return fieldErrors.Count == 0 ? await next(ct) : TResponse.Failure(GeneralErrors.ValidationFailed(fieldErrors));
        }
    }
}
