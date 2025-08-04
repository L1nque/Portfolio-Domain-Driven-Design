using Demo.SharedKernel.Core.Models;

namespace Demo.SharedKernel.Types;

public class DateRange : ValueObject
{
    public DateRange(DateTimeOffset start, DateTimeOffset end)
    {
        Start = start;
        End = end;
    }

    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }

    public int Duration()
    {
        return (End - Start).Days;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Start;
        yield return End;
    }
}
