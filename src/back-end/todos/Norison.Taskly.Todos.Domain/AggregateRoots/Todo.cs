using Norison.Taskly.Todos.Domain.Primitives;
using Norison.Taskly.Todos.Domain.Shared;

namespace Norison.Taskly.Todos.Domain.AggregateRoots;

public class Todo : AggregateRoot
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public TodoStatus Status { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime LastEditedDatetime { get; private set; }

    private Todo() { }

    public Todo(Guid id, string title, string? description, DateTime dateTime)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = TodoStatus.Created;
        CreatedDateTime = dateTime;
        LastEditedDatetime = dateTime;
    }

    public void Update(string name, string? description, DateTime dateTime)
    {
        Title = name;
        Description = description;
        LastEditedDatetime = dateTime;
    }

    public void Complete(DateTime dateTime)
    {
        Status = TodoStatus.Completed;
        LastEditedDatetime = dateTime;
    }

    public void Cancel(DateTime dateTime)
    {
        Status = TodoStatus.Cancelled;
        LastEditedDatetime = dateTime;
    }

    public void Delete(DateTime dateTime)
    {
        Status = TodoStatus.Deleted;
        LastEditedDatetime = dateTime;
    }
}