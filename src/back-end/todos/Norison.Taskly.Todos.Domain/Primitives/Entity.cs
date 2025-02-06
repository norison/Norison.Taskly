namespace Norison.Taskly.Todos.Domain.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; protected set; }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((Entity)obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return Id.GetHashCode();
    }
}