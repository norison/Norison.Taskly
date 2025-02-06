using Norison.Taskly.Todos.Domain.Shared;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;

public class CreateTodoCommandResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public TodoStatus Status { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastEditedDatetime { get; set; }
}