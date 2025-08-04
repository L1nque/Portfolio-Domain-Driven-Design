# Fleet Management

This is a subdomain that handles all the business that revolves around **managing the lifecycle** of a car.

Whether this is a core or a supporting subdomain is a subject of debate. It could be considered **core** if it provides a competitive business advantage, however, we can also argue that maintaining a fleet is the bare minimum expectation and that there is no competitive advantage.

In this doc, I just elaborate on certain design decisions I made, to help you understand my thought process and where I am coming from.

**The intention of this (sub)domain**
- Schedule cars for service
- Ensure that cars are Insured and Road-worthy
- Keep the Car in a consistent state (Available, Maintenance, Unavailable)
- Track the registration of cars

There are not that many invariants in this implementation, although Im sure many more can be added.


## ⚔️ Aggregate Root
While we can encapsulate most of our logic into a single aggregate, I've actually decidedd to create two aggregates in this subdomain.

This still means that they share the same bounded context and ubiquitous language in order to coexist and interact meaningfully.

### Car
The [Car](Car.cs) is going to manage the Physical State and Operational Lifecycle of the car, acting as the single source of truth for everything about the vehicle's physical existence within the fleet.

### Registration
The [Registration](Registration.cs) represents the legal authorization for a vehicle to operate on public roads, issued by a governmental authority, and acts as a "regulatory contract".

The reason why its not a part of the `Car` aggregate, is because a car's legal operability is a separate concern; Registration can outlive a car - plates transferred to a new vehicle - or event exist without a car (reserved plate numbers). If it were treated as a child of `Car` we'd be conflating physical and legal domains.

## ValueObjects
I've covered a lot of this in [RentalContracting](../RentalContracting/README.md) but what I want to highlight is the design decision to make `Insurance` a ValueObject and not an Entity or Aggregate root like `Registration`.

Generally speaking, `Insurance` should have its own bounded-context e.g. `Insurance Management` and the biggest indicator is the language: `Premium`, `Loss Ratio`, `Claim`, `Excess Fee`, `Policy`, etc.

`Fleet Management` sees `Insurance` as a property of the car, that is necessary for compliance and operation and only cares about the expiration date. But there's much more to insurance: Managing financial and legal aspects of policies and claims, and therefore is a completely separate bounded-context.

However, it would still map contextually; `Insurance` in the `Insurance Management` (Upstream) would map to the [InsuranceCompliance](./ValueObjects/InsuranceCompliance.cs) in `Fleet Management` (Downstream).

The reason why `InsuranceCompliance` is not an entity and simply a VO is because it's identity and lifecycle is irrelevant to this context, and its entirely defined by its attributes, even though it has an "ID".

## What could be done better
### File structure
One of the biggest downsides to DDD is the fact that with each "feature" or "behavior" you can have an explosion of files, and in much larger projects, organizing the code (files) is detrimental to the maintainability of the project. An alternative file structure would be to create sub-folders inside each of the folders to "isolate" car-related objects and registration-related objects. Its not a big deal but just worth pointing out.

### Domain Richness
A fleet management context is realistically much more complicated than this. A lot of the complexity and "richness" comes from talking to specialists within the domain: Administrators, Drivers, Managers, etc. and so for the purpose of this demo, I've kept it to a minimum whilst trying to lay down a complex/rich domain.



