using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;

public class CreateTodoCommand : ICommand<Guid>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}