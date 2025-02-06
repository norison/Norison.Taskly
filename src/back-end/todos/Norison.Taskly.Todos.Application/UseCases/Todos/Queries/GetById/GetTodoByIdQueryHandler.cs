using Mapster;

using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetById;

public class GetTodoByIdQueryHandler(IRepository<Todo> todosRepository)
    : IQueryHandler<GetTodoByIdQuery, TodoDto>
{
    public async Task<TodoDto> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await todosRepository.GetAsync(request.Id, cancellationToken);
        return todo.Adapt<TodoDto>();
    }
}