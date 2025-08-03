using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Entities;

/// <summary>
/// The RentalCar entity represents the car being rented within the RentalContracting context.
/// It holds state relevant to the rental process (initial/final odometer/fuel readings)
/// and provides default policies/rates that originate from the FleetManagement context.
/// It is part of the Rental aggregate, managed by the Rental aggregate root.
/// 
/// The RentalCar maps contextually to a Car in the FleetManagement context.
/// </summary>
public class RentalCar : Entity<RentalCarId>
{
    /// <summary>
    /// A snapshot of the kilometrage/mileage at the start of a rental.
    /// This is a contractual/legally binding property that is jotted down
    /// on the physical contract that is signed by the customer/stamped by the company.
    /// It's also used in conjunction with <see cref="FinalOdometer"/> to calculate excess
    /// mileage charges based on the <see cref="MileagePolicy"/>
    /// </summary>
    public OdometerReading InitialOdometer { get; private set; }

    /// <summary>
    /// A snapshot of the kilometrage/mileage at the end of a rental
    /// </summary>
    public OdometerReading? FinalOdometer { get; private set; }

    /// <summary>
    /// A snapshot of the fuel level at the start of a rental.
    /// Another contractual/legally binding property jotted down on the
    /// physical contract.
    /// It is also used (in conjunction with <see cref="EndingFuelLevel"/>) to calculate
    /// refueling fees.
    /// </summary>
    public FuelLevel InitialFuelLevel { get; private set; }

    /// <summary>
    /// A snapshot of the fuel level at the end of a rental.
    /// </summary>
    public FuelLevel? EndingFuelLevel { get; private set; }

    /// <summary>
    /// The stanard MileagePolicy associated with this car,
    /// originating in the FleetManagement context.
    /// </summary>
    public MileagePolicy DefaultMileagePolicy { get; private set; }

    /// <summary>
    /// Gets the standard daily/weekly/monthly rates associated with this car, 
    /// originating from the FleetManagement context.
    /// </summary>
    public RentalRate DefaultRates { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="RentalCar"/> class.
    /// It initializes the car's state and default policies/rates as received from the 
    /// FleetManagement context.
    /// </summary>
    /// <param name="id">The unique identifier for the rental car (ideally maps to a Car in FleetManagement).</param>
    /// <param name="initialOdometerReading">The initial odometer reading.</param>
    /// <param name="initialFuelLevel">The initial fuel level.</param>
    /// <param name="defaultMileagePolicy">The default mileage policy from FleetManagement.</param>
    /// <param name="rentalRate">The default rental rates from FleetManagement.</param>
    public RentalCar(
        RentalCarId id, OdometerReading initialOdometerReading,
        FuelLevel initialFuelLevel, MileagePolicy defaultMileagePolicy, RentalRate rentalRate)
        : base(id)
    {
        InitialFuelLevel = initialFuelLevel;
        InitialOdometer = initialOdometerReading;
        DefaultMileagePolicy = defaultMileagePolicy;
        DefaultRates = rentalRate;
    }

    /// <summary>
    /// Internal method called by the Rental aggregate root when the car is returned.
    /// It records the final state of the car and validates the readings.
    /// Marked 'internal' to restrict access to within the RentalContracting assembly (specifically the Rental aggregate).
    /// </summary>
    /// <param name="finalReading">The final odometer reading.</param>
    /// <param name="finalLevel">The final fuel level.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the final reading is not greater than the initial reading.</exception>
    internal void Return(OdometerReading finalReading, FuelLevel finalLevel)
    {
        if (finalReading <= InitialOdometer)
        {
            throw new ArgumentOutOfRangeException();
        }

        FinalOdometer = finalReading;
        EndingFuelLevel = finalLevel;
    }
}