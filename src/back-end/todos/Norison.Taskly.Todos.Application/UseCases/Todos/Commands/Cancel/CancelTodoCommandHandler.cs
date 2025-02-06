using Mapster;

using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Application.Services.DateNow;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;
using Norison.Taskly.Todos.Domain.Shared.Exceptions;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Cancel;

public class CancelTodoCommandHandler(IRepository<Todo> todosRepository, IDateNowService dateNowService)
    : ICommandHandler<CancelTodoCommand, TodoDto>
{
    public async Task<TodoDto> Handle(CancelTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await todosRepository.GetAsync(request.Id, cancellationToken);

        if (todo is null)
        {
            throw new TasklyException("Todo not found");
        }

        todo.Cancel(dateNowService.Now);
        await todosRepository.UpdateAsync(todo, cancellationToken);
        return todo.Adapt<TodoDto>();
    }
}