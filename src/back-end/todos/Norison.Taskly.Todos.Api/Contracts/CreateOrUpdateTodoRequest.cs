namespace Norison.Taskly.Todos.Api.Contracts;

public class CreateOrUpdateTodoRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}