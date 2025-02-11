using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Update;

public class UpdateTodoCommand : ICommand
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}