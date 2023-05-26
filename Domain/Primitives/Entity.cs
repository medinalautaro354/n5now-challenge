using System;

namespace N5NowApi.Domain.Primitives;
public abstract class Entity : IEquatable<Entity>
{
    protected Entity(int id)
    {
        Id = id;
    }
    public int Id { get; protected set; }

    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return Equals((Entity)other);
    }
}
