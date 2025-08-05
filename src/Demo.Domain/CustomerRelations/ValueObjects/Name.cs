using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName, string[]? middleNames)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleNames = middleNames;
    }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string[]? MiddleNames { get; init; }
    public string FullName
    {
        get
        {
            if (MiddleNames != null && MiddleNames.Length > 0)
            {
                return $"{FirstName} {string.Join(" ", MiddleNames)} {LastName}";
            }

            return $"{FirstName} {LastName}";
        }
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return FirstName;
        yield return MiddleNames;
        yield return LastName;
    }
}