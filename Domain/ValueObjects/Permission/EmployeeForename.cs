using N5NowApi.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace N5NowApi.Domain.ValueObjects.Permission;

public sealed class EmployeeForename : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    private EmployeeForename(string value)
    {
        Value = value;
    }

    public static EmployeeForename Create(string employeeForename)
    {
        if (string.IsNullOrWhiteSpace(employeeForename))
        {
            throw new ArgumentNullException(nameof(employeeForename));
        }

        return new EmployeeForename(employeeForename);
    }
}

