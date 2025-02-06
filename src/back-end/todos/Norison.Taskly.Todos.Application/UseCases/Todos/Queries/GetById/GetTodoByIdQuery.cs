using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetById;

public class GetTodoByIdQuery : IQuery<TodoDto>
{
    public Guid Id { get; set; }
}