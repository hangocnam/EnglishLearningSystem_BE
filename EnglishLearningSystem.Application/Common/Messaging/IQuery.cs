using EnglishLearningSystem.Domain.Common.Results;
using MediatR;

namespace EnglishLearningSystem.Application.Common.Messaging
{
    /// <summary>Read-only request. Response is always <see cref="Result{T}"/>.</summary>
    public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }

    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse> { }
}
