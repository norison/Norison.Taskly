using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;

public class CreateTodoCommand : ICommand<TodoDto>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}