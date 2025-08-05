using Demo.Domain.CustomerRelations.Entities;
using Demo.Domain.CustomerRelations.Enums;
using Demo.Domain.CustomerRelations.Events;
using Demo.Domain.CustomerRelations.Interfaces;
using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.CustomerRelations;

public class Customer : AggregateRoot<CustomerId>
{
    private readonly List<IdentityDocument> _documents = new();

    /// <summary>
    /// ValueObject that encapsulates all concepts related to a customer's address. 
    /// </summary>
    public Address Address { get; private set; }

    /// <summary>
    /// Customer related personal information
    /// </summary>
    public PersonalInformation PersonalInformation { get; private set; }

    /// <summary>
    /// ValueObject that encapsulates all concepts related to a customer's contact info.    
    /// </summary>
    public ContactInformation ContactInformation { get; private set; }

    /// <summary>
    /// Snapshot of the customer's risk profile, provided by a different BC
    /// e.g. "Risk & Compliance" (upstream/downstream relationship)
    /// </summary>
    public RiskProfile RiskProfile { get; private set; }

    /// <summary>
    /// All/Any of the customers docs: passport, license, id, etc.
    /// </summary>
    public IReadOnlyCollection<IdentityDocument> Documents => _documents.AsReadOnly();

    /// <summary>
    /// Customers engagement/behavior. Can possibly be extracted as an AggregateRoot
    /// or even a separate BC depending on complexity.
    /// </summary>
    public BehaviorProfile? BehaviorProfile { get; private set; }

    /// <summary>
    /// Whether the customer is verified or not.
    /// </summary>
    public CustomerVerificationStatus VerificationStatus { get; private set; }

    public Customer(
        PersonalInformation personalInformation, ContactInformation contactInformation,
        Address address, RiskProfile? riskProfile = null)
    {
        Address = address;
        PersonalInformation = personalInformation;
        ContactInformation = contactInformation;
        RiskProfile = riskProfile ?? new RiskProfile(RiskLevel.Low);
    }

    /// <summary>
    /// Can possibly be used to enforce that an address cannot be changed
    /// if there is a car actively being delivered.
    /// </summary>
    /// <param name="newAddress"></param>
    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress;
        AddDomainEvent(new CustomerAddressUpdatedEvent(Id, newAddress));
    }

    public void UpdatePersonalInformation(PersonalInformation newInformation)
    {
        PersonalInformation = newInformation;
        AddDomainEvent(new CustomerPersonalInformationUpdatedEvent(Id, newInformation));
    }

    public void UpdateContactInformation(ContactInformation contactInformation)
    {
        ContactInformation = contactInformation;
        AddDomainEvent(new CustomerContactInformationUpdatedEvent(Id, contactInformation));
    }

    public void UpdateRiskProfile(RiskProfile riskProfile)
    {
        RiskProfile = riskProfile;
        AddDomainEvent(new CustomerRiskProfileUpdatedEvent(Id, riskProfile));
    }

    public void AddIdentityDocument(IdentityDocument document)
    {
        if (Documents.FirstOrDefault(d => d.Number == document.Number) != null)
        {
            throw new Exception("Duplicate Document");
        }

        _documents.Add(document);
        AddDomainEvent(new CustomerIdentityDocumentAddedEvent(Id, document));
    }

    public void UpdateBehaviorProfile(BehaviorProfile profile)
    {
        BehaviorProfile = profile;
        AddDomainEvent(new CustomerBehaviorProfileUpdatedEvent(Id, profile));
    }

    /// <summary>
    /// I was tempted to use <see cref="ICustomerVerificationPolicy"/> directly here
    /// but that would make this aggregate not "DDD Pure" as it would depend on 
    /// something external to it.
    /// 
    /// Accepting data is different, because the aggregate will make decisions
    /// based on its current state; the aggregate isn't calling any external code,
    /// it's using the provided data to perform its logic.
    /// 
    /// In other words: we pass Data, not behavior.
    /// </summary>
    /// <param name="verificationResult"></param>
    public void VerifyCustomer(VerificationResult verificationResult)
    {
        if (VerificationStatus == CustomerVerificationStatus.Verified)
            throw new Exception("Attempting to verify an already verified customer.");

        if (verificationResult.IsVerified)
        {
            VerificationStatus = CustomerVerificationStatus.Verified;
            AddDomainEvent(new CustomerVerifiedEvent(Id));
        }
    }
}