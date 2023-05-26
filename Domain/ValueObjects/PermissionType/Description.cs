using N5NowApi.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace N5NowApi.Domain.ValueObjects.PermissionType;
public sealed class Description : ValueObject
{
    public const int MaxLength = 255;
    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    private Description(string value)
    {
        Value = value;
    }

    public static Description Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
        }

        return new Description(description);
    }
}

