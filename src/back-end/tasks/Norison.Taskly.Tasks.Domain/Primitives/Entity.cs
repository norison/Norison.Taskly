namespace Norison.Taskly.Tasks.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; protected set; }
    
    public bool Equals(Entity? other)
    {
        if (other is null || GetType() != other.GetType())
        {
            return false;
        }
        
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        
        return Id == other.Id;
    }
}