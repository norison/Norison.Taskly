using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Application.Services.DateNow;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Update;

public class UpdateTodoCommandHandler(IRepository<Todo> todosRepository, IDateNowService dateNowService)
    : ICommandHandler<UpdateTodoCommand>
{
    public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await todosRepository.GetAsync(request.Id, cancellationToken);
        todo.Update(request.Title, request.Description, dateNowService.Now);
        await todosRepository.UpdateAsync(todo, cancellationToken);
    }
}