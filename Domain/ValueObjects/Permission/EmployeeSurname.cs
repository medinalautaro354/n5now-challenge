using N5NowApi.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace N5NowApi.Domain.ValueObjects.Permission;

public sealed class EmployeeSurname : ValueObject
{
    public const int MaxLength = 50;
    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    private EmployeeSurname(string value)
    {
        Value = value;
    }

    public static EmployeeSurname Create(string employeeSurname)
    {
        if (string.IsNullOrWhiteSpace(employeeSurname))
        {
            throw new ArgumentNullException(nameof(employeeSurname));
        }

        return new EmployeeSurname(employeeSurname);
    }
}

