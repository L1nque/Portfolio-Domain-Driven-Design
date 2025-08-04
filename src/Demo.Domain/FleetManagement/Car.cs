using Demo.Domain.FleetManagement.Entities;
using Demo.Domain.FleetManagement.Enums;
using Demo.Domain.FleetManagement.Events;
using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;


namespace Demo.Domain.FleetManagement;

/// <summary>
/// This class acts as one of two aggregate roots within the Fleet Management
/// subdomain.
/// 
/// <para>
///     Manages the physical state and operational lifecycle of the car,
///     acting as the single source of truth for everything about its
///     physical existence within the fleet.
/// </para>
/// </summary>
public class Car : AggregateRoot<CarId>
{
    /// <summary>
    /// Private collection to record any reported damage to the car
    /// </summary>
    private readonly List<Damage> _damages = new();
    /// <summary>
    /// Private collection that records the service history of the car.
    /// </summary>
    private readonly List<ServiceLog> _serviceHistory = new();

    /// <summary>
    /// VO encapsulating Make, Model, Year
    /// </summary>
    public CarModel Model { get; private set; }

    /// <summary>
    /// Represents the current Mileage/Kilometrage of the car,
    /// which can be logged manually or via telemetry.
    /// </summary>
    public OdometerReading Odometer { get; private set; }

    /// <summary>
    /// Reference to the car's registration. A car is not necessarily
    /// registered.
    /// </summary>
    public RegistrationId? RegistrationId { get; private set; }

    /// <summary>
    /// The insurance of the car. A car is not necessarily insured.
    /// </summary>
    public InsuranceCompliance? Insurance { get; private set; }

    /// <summary>
    /// Transitions of the car (Available, Rented, etc.)
    /// </summary>
    public CarStatus Status { get; private set; }

    /// <summary>
    /// The VIN of the car. Each car has a single unique vin
    /// that should always be immutable
    /// </summary>
    public Vin Vin { get; private set; }

    /// <summary>
    /// Represents the details of the FuelTank of a car.
    /// </summary>
    public FuelTank Fuel { get; private set; }

    /// <summary>
    /// Indicates whether a car is due for cleaning. Ideally,
    /// create a domain object to represent this e.g. a ValueObject
    /// </summary>
    public bool RequiresCleaning { get; private set; }

    /// <summary>
    /// <see cref="_damages"/>
    /// </summary>
    public IReadOnlyCollection<Damage> Damages => _damages.AsReadOnly();

    /// <summary>
    /// <see cref="_serviceHistory"/>
    /// </summary>
    public IReadOnlyCollection<ServiceLog> ServiceHistory => _serviceHistory.AsReadOnly();

    /// <summary>
    /// Initializes a car, ideally, if there were any invariants to be enforced upon creation
    /// we would use a factory method as it would also represent the ubiquitous language
    /// </summary>
    /// <param name="vin">The 17 character VIN</param>
    /// <param name="model">Make, Model, Year</param>
    /// <param name="odometer">Km/Mileage</param>
    /// <param name="fuel">Fuel level</param>
    public Car(Vin vin, CarModel model, OdometerReading odometer, FuelTank fuel)
    {
        Vin = vin;
        Model = model;
        Odometer = odometer;
        Fuel = fuel;
    }

    /// <summary>
    /// Attaches a registration to a car
    /// </summary>
    /// <param name="registrationId"></param>
    public void RegisterCar(RegistrationId registrationId)
    {
        RegistrationId = registrationId;

        AddDomainEvent(new CarRegisteredEvent(Id, registrationId));
    }

    /// <summary>
    /// Adds insurance to a car
    /// </summary>
    /// <param name="insurance"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void InsureCar(InsuranceCompliance insurance)
    {
        if (RegistrationId == null)
        {
            throw new InvalidOperationException();
        }

        Insurance = insurance;
        AddDomainEvent(new CarInsuredEvent(Id, Insurance.ExpirationDate));
    }

    /// <summary>
    /// Renews the insurance of a car. This will likely be a new VO
    /// as they are immutable, however, FleetManagement does not car about
    /// maintaining a history log of insurance. That would probably be a
    /// responsibility for a different subdomain e.g. `Insurance Management`.
    /// </summary>
    /// <param name="insurance"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void RenewInsurance(InsuranceCompliance insurance)
    {
        if (Insurance is null)
        {
            throw new InvalidOperationException();
        }

        Insurance = insurance;
        AddDomainEvent(new CarInsuredEvent(Id, Insurance.ExpirationDate));
    }


    /// <summary>
    /// Marks the car for servicing, changing its status.
    /// </summary>
    public void ServiceCar()
    {
        Status = CarStatus.Maintenance;
        AddDomainEvent(new CarStatusChangedEvent(Id, Status));
    }

    /// <summary>
    /// After a car has been serviced, it then becomes available
    /// and we update its next service threshold (mileage at which
    /// it needs to be serviced.)
    /// </summary>
    /// <param name="serviceLog"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void CompleteService(ServiceLog serviceLog)
    {
        if (Status != CarStatus.Maintenance)
        {
            throw new InvalidOperationException();
        }

        Status = CarStatus.Available;
        _serviceHistory.Add(serviceLog);
        Odometer = OdometerReading.From(Odometer, nextServiceThreshold: serviceLog.NextServiceThreshold);

        AddDomainEvent(new CarStatusChangedEvent(Id, Status));
        AddDomainEvent(new CarServiceCompletedEvent(Id, serviceLog.Id));
    }

    /// <summary>
    /// Updates the mileage.
    /// 
    /// This can be manual input via an employee, or automatic
    /// through telemetry (OBS device)
    /// </summary>
    /// <param name="value"></param>
    public void UpdateMileage(float value)
    {
        Odometer = OdometerReading.From(Odometer, value: value);
        AddDomainEvent(new CarOdometerUpdatedEvent(Id, value));
    }

    /// <summary>
    /// Reports damage to the car.
    /// </summary>
    /// <param name="damage"></param>
    public void ReportDamage(Damage damage)
    {
        _damages.Add(damage);
    }

    /// <summary>
    /// <see cref="ReportDamage"/>
    /// </summary>
    /// <param name="damages"></param>
    public void ReportDamage(IEnumerable<Damage> damages)
    {
        _damages.AddRange(damages);
    }
}