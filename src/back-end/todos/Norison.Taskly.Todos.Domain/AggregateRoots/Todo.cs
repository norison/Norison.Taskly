using System.ComponentModel.DataAnnotations;

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

    public void Update(string title, string? description, DateTime dateTime)
    {
        Title = title;
        Description = description;
        LastEditedDatetime = dateTime;
    }

    public void ChangeStatus(TodoStatus status)
    {
        switch (status)
        {
            case TodoStatus.Created:
                throw new ValidationException("Cannot change status to created");
            case TodoStatus.Completed or TodoStatus.Cancelled when Status == TodoStatus.Created:
                throw new ValidationException("This status can be applied only for created status");
        }

        Status = status;
    }
}