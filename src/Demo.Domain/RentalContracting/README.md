# Rental Contracting

This is a <u>core subdomain</u> that handles all the business that revolves around **managing the lifecycle** of a rental.

In this doc, I just elaborate on certain design decisions I made, to help you understand my thought process and where I am coming from.

**The intention of this (sub)domain**

A car is rented out. the rental needs to track:
- The Mileage at check-in and check-out
- The fuel level at check-in and check-out
- The mileage policy agreed upon. Defaults to the car's mileage policy, but can be overridden.
- The fuel policy agreed upon. 
- The rental rates. Defaults to the cars rates, but can be overridden.

A rental has one of 5 states that it can be in at any given time:
- `Open` (perhaps via booking, initializing the rental)
- `InProgress` (contract finalized, customer signed, company stamped)
- `Stopped` (car returned, but rental is not concluded e.g. outstanding balance)
- `Closed` (rental has been concluded)
- `Cancelled` (An open rental that was never finalized)

There are many invariants and we won't touch all of them but some examples inlcude:
- A rentals mileage policy cannot be modified once the rental has started (because the customer signed the physical contract, and the company stamped it)
- The rates (daily/weekly/monthly) cannot be modified once a rental has started
- Can't cancel a rental that has progressed from `Open` to `InProgress`


**🗣️ The ubiquitous language includes:**
 
- `Rental`, `Period`, `Extension`, `Cancellation`, `Status` 
- `Driver`, `Secondary Driver`
- `Car`, `Mileage Allowance`, `Excess Mileage`, `Fuel Level`

**📦 Behaviors & Encapsulation:**
- Manages the lifecycle of a rental: Creation, extensions, completion, and cancellation.
- Enforces invariants (minimum duration, driver eligibility)
- Snapshots the contract's terms and state (initial fuel level, odometer reading, etc.)

**🗺️ Context Mapping:**
- A `Driver` in this context, is the person that rented the car (is driving the car). This maps to a `Customer` in the `CustomerRelations` bounded context - they both are the same (by ID) but mean two different things depending on the context.
- A `RentalCar` in this context, is the car that is being rented out - we care about the Initial/Ending Fuel Level and Mileage - whereas in the `FleetManagement` context, there is no concept of Initial/Ending fuel level and mileage, just "current" mileage. So again, what a "car" is, different depending on the context but they map to each other.

## ⚔️ Aggregate Root
The [Rental](./Rental.cs) is going to act as the root that guards the consistency of this boundary (rental contracting) and enforce the invariants.

When a car is rented out, on the physical contract, the company writes the current fuel level and mileage of the car; we ask ourselves the question: "Once the contract is finalized and the mileage/fuel level is jotted down, can we later change the initial mileage/fuel level?" the answer is probably: no.

So even though we have an [Entity that represents a Car](Entities/RentalCar.cs), the `Rental` acts as the consistency boundary that says "This rental is active and the initial fuel level/mileage cannot be changed."

I always like to try and meet the following 3 criteria when deisgning an aggregate:

1. It shouldn't be an anemic domain model
    - Not just a data model, but a rich domain model
2. It shouldn't model your database
    - We are modelling our business, not database tables
3. The behavior focuses on mutations and state transitions, not query logic.
    - tactical DDD focuses on commands/writes/mutations and not the query side of things. We can model projections for custom query-models, but that's not part of our domain.

### Entities
The [RentalCar](Entities/RentalCar.cs) and the [Driver](Entities/Driver.cs) may seem like they are `ValueObjects` as they store a "Snapshot". But they have an identity and they evolve over the state transitions of the rental (e.g. `InitialFuelLevel` vs `EndingFuelLevel`) which makes them entities.

One way I look at this is (considering ValueObjects have no `ID`):
"is a Driver named Michael the same as this other Driver named Michael?" - the answer is no. Two drivers can have the same name, but are different (people). 

### ValueObjects

You can read more about ValueObjects in the [Shared Kernel](../../Demo.SharedKernel/README.md) but what I want to touch on is [OdometerReading.cs](./ValueObjects/OdometerReading.cs)

You might think that this is a very similar model to the one in the [FleetManagement.OdometerReading](../FleetManagement/ValueObjects/OdometerReading.cs) and so you wonder why I violate the [DRY](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself) principle. You might even suggest that we should just move it to the `SharedKernel` and have both contexts reference it.

And, you'd be wrong: Bounded Contexts is why.

Yes, both encapsulate Mileage/Kilometrage providing a wrapper around the value and the unit of measure, however, their semantic meaning differs.

**In `FleetManagement`**

The OdomoterReading is treated as current *live telemetry*. "Current Odometer" - proivdes the most recent known mileage/kilometrage for a given car (regardless if it is rented or not) and this info can be fed from an external api or manual input. It purpose can be to trigger maintenance reminders, calculate wear and tear, etc.

**In `RentalContracting`**

The OdometerReading is treated as a *frozen, legally binding data point from inpsection*. Its a human-confirmed mileage value at the start/end of a rental. It isn't updated - its a Snapshot.

---

The reason this is important is because a change in one context's meaning, might break the other. What if there appears to be a business requirement: to have the employee/customer take a photo as evidence for the OdometerReading so we add the following property:

```csharp
// URL to photo of odometer reading stored on S3.
// Used during disputes for excess mileage charges
// or Fuel Level difference disputes.
public string EvidenceUrl { get; private set; }
```

If this was shared by the `FleetManagement` context, well guess what? `EvidenceUrl` means **nothing** to it, and it would have to ignore it, leading to semantic pollution, which is the very problem that bounded-contexts aim to prevent.

So it's just better to create two different models for the same concept, bind them by their language and semantics, and let them evolve separately.