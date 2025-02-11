namespace Norison.Taskly.Todos.Api.Contracts;

public class CreateOrUpdateTodoRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}