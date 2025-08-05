using Demo.Domain.CustomerRelations.Enums;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class CommunicationPreferences : ValueObject
{
    public List<CommunicationMethod> PreferredMethods { get; }
    public bool EmailOptIn { get; }
    public bool SmsOptIn { get; }
    public bool MarketingOptIn { get; }
    public string PreferredLanguage { get; }
    public TimeZoneInfo PreferredTimeZone { get; }

    public CommunicationPreferences(
        List<CommunicationMethod> preferredMethods = null!,
        bool emailOptIn = true,
        bool smsOptIn = false,
        bool marketingOptIn = false,
        string preferredLanguage = "en-US",
        TimeZoneInfo preferredTimeZone = null!)
    {
        PreferredMethods = preferredMethods ?? new List<CommunicationMethod> { CommunicationMethod.Email };
        EmailOptIn = emailOptIn;
        SmsOptIn = smsOptIn;
        MarketingOptIn = marketingOptIn;
        PreferredLanguage = preferredLanguage;
        PreferredTimeZone = preferredTimeZone ?? TimeZoneInfo.Utc;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        foreach (var method in PreferredMethods)
        {
            yield return method;
        }

        yield return EmailOptIn;
        yield return SmsOptIn;
        yield return MarketingOptIn;
        yield return PreferredLanguage;
        yield return PreferredTimeZone;
    }
}
