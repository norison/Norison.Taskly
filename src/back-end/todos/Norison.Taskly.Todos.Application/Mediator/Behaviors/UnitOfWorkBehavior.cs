using System.Data;

using MediatR;

using Norison.Taskly.Todos.Application.Interfaces;

namespace Norison.Taskly.Todos.Application.Mediator.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        IDbTransaction? transaction = null;

        if (IsCommand(request))
        {
            transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        }

        var response = await next();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        transaction?.Commit();
        return response;
    }

    private static bool IsCommand(TRequest request)
    {
        var requestType = request.GetType();
        return typeof(ICommand).IsAssignableFrom(requestType) || typeof(ICommand<>).IsAssignableFrom(requestType);
    }
}