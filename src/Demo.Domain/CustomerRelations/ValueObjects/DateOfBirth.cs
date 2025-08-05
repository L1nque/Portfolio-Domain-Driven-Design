using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class DateOfBirth : ValueObject
{
    public DateOfBirth(DateOnly value)
    {
        Value = value;
    }

    public const int MaxAge = 120;

    public DateOnly Value { get; }

    public int GetAge(DateOnly today)
    {
        int age = today.Year - Value.Year;

        if (Value.DayOfYear > today.DayOfYear)
        {
            age--;
        }

        return age;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }
}
