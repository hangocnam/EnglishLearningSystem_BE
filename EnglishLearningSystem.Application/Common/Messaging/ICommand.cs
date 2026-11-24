using EnglishLearningSystem.Domain.Common.Results;
using MediatR;

namespace EnglishLearningSystem.Application.Common.Messaging
{
    /// <summary>Command that changes state and returns no data. Response is always <see cref="Result"/>.</summary>
    public interface ICommand : IRequest<Result> { }

    /// <summary>Command that changes state and returns data. Response is always <see cref="Result{T}"/>.</summary>
    public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand { }

    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse> { }
}
