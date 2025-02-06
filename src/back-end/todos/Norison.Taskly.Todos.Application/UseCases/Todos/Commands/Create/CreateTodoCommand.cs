using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;

public class CreateTodoCommand : ICommand<CreateTodoCommandResult>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}