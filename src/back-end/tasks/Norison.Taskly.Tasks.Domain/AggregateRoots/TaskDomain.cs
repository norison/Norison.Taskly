using Norison.Taskly.Tasks.Domain.Primitives;

namespace Norison.Taskly.Tasks.Domain.AggregateRoots;

public class TaskDomain(Guid id, string name, string description) : AggregateRoot(id)
{
    public string Name { get; } = name;
    public string Description { get;} = description;
    public TaskStatus Status { get; } = TaskStatus.Created;
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastEditedDatetime { get; set; }
}