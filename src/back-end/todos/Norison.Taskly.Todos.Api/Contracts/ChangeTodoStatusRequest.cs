using Norison.Taskly.Todos.Domain.Shared;

namespace Norison.Taskly.Todos.Api.Contracts;

public class ChangeTodoStatusRequest
{
    public TodoStatus Status { get; set; }
}