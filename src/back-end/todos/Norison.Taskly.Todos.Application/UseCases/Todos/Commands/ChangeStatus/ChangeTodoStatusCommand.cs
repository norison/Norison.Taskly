using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Domain.Shared;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.ChangeStatus;

public class ChangeTodoStatusCommand : ICommand
{
    public Guid Id { get; set; }
    public TodoStatus Status { get; set; }
}