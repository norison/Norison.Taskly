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
        var requestType = request.GetType();

        if (requestType != typeof(ICommand) && requestType != typeof(ICommand<>))
        {
            return await next();
        }

        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        var response = await next();
        transaction.Commit();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return response;
    }
}