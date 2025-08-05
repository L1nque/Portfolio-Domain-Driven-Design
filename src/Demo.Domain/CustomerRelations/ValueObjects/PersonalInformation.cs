using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class PersonalInformation : ValueObject
{
    public PersonalInformation(Name name, DateOfBirth dateOfBirth, Nationality nationality)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
    }

    public Name Name { get; }
    public DateOfBirth DateOfBirth { get; }
    public Nationality Nationality { get; }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Name;
        yield return DateOfBirth;
        yield return Nationality;
    }
}