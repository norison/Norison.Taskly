using Norison.Taskly.Todos.Domain.Shared;

namespace Norison.Taskly.Todos.Application.DTOs;

public class TodoDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public TodoStatus Status { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastEditedDatetime { get; set; }
}