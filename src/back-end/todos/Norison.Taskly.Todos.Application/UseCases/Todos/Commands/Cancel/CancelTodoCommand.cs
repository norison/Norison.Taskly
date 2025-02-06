using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Cancel;

public class CancelTodoCommand : ICommand<TodoDto>
{
    public Guid Id { get; set; }
}