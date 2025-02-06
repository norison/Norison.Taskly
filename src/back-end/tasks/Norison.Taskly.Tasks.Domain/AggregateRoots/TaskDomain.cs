using Norison.Taskly.Tasks.Domain.Primitives;

namespace Norison.Taskly.Tasks.Domain.AggregateRoots;

public class TaskDomain : AggregateRoot
{
    public string Name { get; }
    public string Description { get;}
    public TaskStatus Status { get; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastEditedDatetime { get; set; }
    
    public TaskDomain(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
        Status = TaskStatus.Created;
    }
}