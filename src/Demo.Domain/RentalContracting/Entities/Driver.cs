using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Extensions;
using Demo.Domain.RentalContracting.Exceptions;
using Demo.Domain.RentalContracting.ValueObjects;

namespace Demo.Domain.RentalContracting.Entities;

/// <summary>
/// The Driver entity represents a customer within the RentalContracting context.
/// It holds driver-specific information and enforces eligibility rules.
/// It is part of the Rental aggregate, managed by the Rental aggregate root.
/// It maps contextually to a Customer entity in the CustomerRelations bounded context.
/// </summary>
public class Driver : Entity<DriverId>
{
    /// <summary>
    /// Minimum driver age
    /// </summary>
    public static readonly int MinimumAge = 25;

    /// <summary>
    /// Minimum number of days the driver must have held their license
    /// </summary>
    public static readonly int MinimumLicenseAge = 365;


    /// <summary>
    /// Driver's date of birth, used to enforce invariants
    /// </summary>
    public DateTime DateOfBirth { get; private set; }

    /// <summary>
    /// license issue date, used to enforce invariants
    /// </summary>
    public DateTime LicenseIssueDate { get; private set; }

    /// <summary>
    /// license expiry date, used to enforce invariants
    /// </summary>
    public DateTime LicenseExpiryDate { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="Driver"/> class.
    /// Initializes the driver's state as received from the CustomerRelations bounded-context.
    /// </summary>
    /// <param name="id">The unique identifier for the driver.</param>
    /// <param name="dateOfBirth">The driver's date of birth.</param>
    /// <param name="licenseIssueDate">The date the driver's license was issued.</param>
    /// <param name="licenseExpiryDate">The date the driver's license expires.</param>
    public Driver(DriverId id, DateTime dateOfBirth, DateTime licenseIssueDate, DateTime licenseExpiryDate)
        : base(id)
    {
        DateOfBirth = dateOfBirth;
        LicenseIssueDate = licenseIssueDate;
        LicenseExpiryDate = licenseExpiryDate;
    }


    /// <summary>
    /// Validates the driver's age against the minimum requirement.
    /// Returns the Driver instance itself to allow for method chaining (fluent interface).
    /// 
    /// This isn't necessary, but I just thought it'd be cool.
    /// </summary>
    /// <returns>The current <see cref="Driver"/> instance if validation passes.</returns>
    /// <exception cref="DriverAgeException">Thrown if the driver is below the minimum age.</exception>
    public Driver CheckAge()
    {
        if (DateOfBirth.CalculateAge() < MinimumAge)
        {
            throw new DriverAgeException();
        }

        return this;
    }

    /// <summary>
    /// Validates the driver's license status.
    /// Ensures the license is not expired and has been held for the minimum required time.
    /// Returns the Driver instance itself to allow for method chaining.
    /// </summary>
    /// <returns>The current <see cref="Driver"/> instance if validation passes.</returns>
    /// <exception cref="DriverDocumentInvalidException">Thrown if the license is expired or too new.</exception>
    public Driver CheckLicense()
    {
        if (LicenseExpiryDate < DateTimeOffset.UtcNow)
        {
            throw new DriverDocumentInvalidException("License is expired");
        }

        if ((DateTimeOffset.UtcNow - LicenseIssueDate).TotalDays < 365)
        {
            throw new DriverDocumentInvalidException("License age has not matured");
        }

        return this;
    }
}