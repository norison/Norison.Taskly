using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.ChangeStatus;

public class ChangeTodoStatusCommandHandler(IRepository<Todo> todosRepository)
    : ICommandHandler<ChangeTodoStatusCommand>
{
    public async Task Handle(ChangeTodoStatusCommand request, CancellationToken cancellationToken)
    {
        var todo = await todosRepository.GetAsync(request.Id, cancellationToken);
        todo.ChangeStatus(request.Status);
        await todosRepository.UpdateAsync(todo, cancellationToken);
    }
}