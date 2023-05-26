using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5NowApi.Domain.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject other)
    {
        return other is not null && ValuesAreEquals(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEquals(other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int),
                HashCode.Combine);
    }
    private bool ValuesAreEquals(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}
