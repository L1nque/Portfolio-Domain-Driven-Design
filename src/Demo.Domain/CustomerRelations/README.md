# Customer Relations

This is a subdomain that handles all of the business that revolves around **managing the life-cycle** of a customer.

This doc just walks through the design decisions that I made -- thinking out loud.

**The intention of this (sub)domain**
- Verify Customers
- Estimate Risk
- Understand engagement/behavior of renting cars
- Keep the customer in a consistent state

There are many more business rules and invariants that can be added to this subdomain to make it much more behavioral and less anemic, however, for all intents and purposes, while I have complicated the design significantly, I've cut down on a lot of things as this is for educational purposes.

## ⚔️ AggregateRoot
The [Customer](./Customer.cs) is going to act as the root that guards the consistency of this boundary (Customer Relations) and enforce its invariants. Unlike the [Fleet Management context](../FleetManagement/README.md), this boundary has a single AggregateRoot, however, there are areas that we could refactor or extract and add additional AR's depending on the complexity of the business logic. One similar case here is the [BehaviorProfile](./ValueObjects/BehaviorProfile.cs) which is currently modeled as a ValueObject but can easily be justified to be an AggregateRoot or even refactored into its own Bounded Context.

### Entities
The [IdentityDocument](./Entities/IdentityDocument.cs) is the only concept that fit the criteria to be an entity as opposed to the rest of the ValueObjects in this bounded-context. It has a lifecycle, state, and identity and no independent business existence outside the context of a customer.

### ValueObjects
You can read more about ValueObjects in the [Shared Kernel](../../Demo.SharedKernel/README.md) but I want to touch upon is the [Behavior Profile](./ValueObjects/BehaviorProfile.cs).

As it stands, this ValueObject simply stores basic facts about the customer's engagmenet and (rentals) usage. That's been deliberately done for the purpose of this project. However, in a more realistic scenario, the Behavior profile could expand to have additional fields, complex business, rules, or even invariants and state transitions. 

If it were to evolve to have some form of rich analytics with a lot of metrics & insights, we could instead create an aggregate root for it to enforce its own evolution over time. More so, if "Customer Behavior Tracking" grows with much more complex domain requirements, such as "Loyalty Programs", "Churn Prediction", "ML/AI Algorithms", etc. it would warrant the option to split it into its own Bounded Context - for example: "Behavior Analsis".

### Services
You can learn more about Domain services in the main [README](../../../README.md) but in this context, I want to touch upon the [CustomerVerificationPolicy](./Services/CustomerVerificationPolicy.cs).

In DDD, policies are modeled *explicitly* when the business language already uses the term e.g. "Verification Policy", "Refund Policy", etc. In one way or the other, they are still considered to be a domain service but they're an excellent abstraction for encapsulating rules that can vary by country, tenant, product, etc. and isolates the rule from the service that execeutes it. Due to the nature of DI, this also means that it's extensible and we can swap out different policy implementations depending on the context (e.g. a country could have specific verification policies for car rental customers).

You might wonder what then is the purpose of [CustomerVerificationService](./Services/CustomerVerificationService.cs). A policy answers the binary yes/no question, whereas the service handles the coordination, e.g. multiple policies, combining results, returning richer outcomes. The policy is the rule, and the service is the orchestration around the rule.