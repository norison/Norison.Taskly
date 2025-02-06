using Mapster;

using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Application.Services.DateNow;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;
using Norison.Taskly.Todos.Domain.Shared.Exceptions;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Update;

public class UpdateTodoCommandHandler(IRepository<Todo> todosRepository, IDateNowService dateNowService)
    : ICommandHandler<UpdateTodoCommand, TodoDto>
{
    public async Task<TodoDto> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await todosRepository.GetAsync(request.Id, cancellationToken);

        if (todo is null)
        {
            throw new TasklyException("Todo not found");
        }

        todo.Update(request.Name, request.Description, dateNowService.Now);
        await todosRepository.UpdateAsync(todo, cancellationToken);
        return todo.Adapt<TodoDto>();
    }
}