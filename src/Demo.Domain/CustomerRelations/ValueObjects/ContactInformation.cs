using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class ContactInformation : ValueObject
{
    public ContactInformation(Email email, List<Phone> phoneNumbers, CommunicationPreferences communicationPreferences)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumbers = phoneNumbers ?? new List<Phone>();
        CommunicationPreferences = communicationPreferences ?? new CommunicationPreferences();
    }

    public Email Email { get; }
    public List<Phone> PhoneNumbers { get; }
    public CommunicationPreferences CommunicationPreferences { get; }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Email;

        foreach (Phone phone in PhoneNumbers)
        {
            yield return phone;
        }
    }
}
