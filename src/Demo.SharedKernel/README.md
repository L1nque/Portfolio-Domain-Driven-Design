# Shared Kernel
The **Shared Kernel** is a central part of this project and provides common domain concepts, cross-cutting utilities, and foundational building blocks shared across multiple bounded contexts. It promotes consistency, reduces duplication, and enforces domain modeling best practices.

It contains a collection of **domain-specific types** and **technical utilities** that are:

- **Universal** across all layers (Domain, Application, Infrastructure, Web)
- **Stable** and change-resistant
- **Independent** of any specific business subdomain

> It is **not** a dumping ground for "common" code, but a carefully curated set of abstractions and primitives that are truly shared across all layers and contexts.

The majority of the code and classes here represents the different aspects of tactical Domain-Driven design such as Aggregates, Entities & ValueObjects.

**Best Practices**
- The shared model is stable and unlikely to change or cause breaking-changes. `Money` or `Address` object is a good candidate. A `Car` or `Customer` probably isn't because they can mean very different things to different contexts.
- If the overhead of translation is genuinely high and we would need a complex ACL, then the SharedKernel is better.

## AggregateRoot
The [AggregateRoot](Core/Models/AggregateRoot.cs) class is an abstract base class for implementing the [Aggregate Root Pattern](https://deviq.com/domain-driven-design/aggregate-pattern) that servers as the entry point for operations on its cluster of associated objects, and acts as the consistency boundary.

The `AggregateRoot` should always be identified uniquely and is responsible for raising domain events.

#### Usage
```csharp
public class Example : AggregateRoot<ExampleId>
```

Two things that I keep in mind that help me model aggregates are:

#### 1. You are not modelling 'data'
A good aggregate root exposes **behavioral** methods that represent the business and uses ubiquitous language. `Car.SetFuelLevel()` might seem straight-forward, but the DDD approach would look something like `Car.Refuel()` and this has certain impilications (e.g. "Set" fuel level indicates we can increase/decrease the fuel level)

#### 2. Aggregate models do not need to conform to persistence models
When you are designing an aggregate, you're not trying to model your database tables. Your trying to model the business. *Persistence Models* are an infrastructural concern.

> There's a lot more to AggregateRoots, please research the pattern to fully understand its role. These 2 points are just something that has helped me enhance my way of thinking when designing aggregates


## DomainEvent
The [DomainEvent](Core/Models/DomainEvent.cs) abstract class provides a common structure for domain events raised by an aggregate. In order to react to those events, we would use a pub/sub system (or the [observer pattern](https://refactoring.guru/design-patterns/observer)), but instead of creating our own mechansim for this, I opted for using MediatR instead. 

While I do understand that we want to minimize and possibly eliminate third-party dependencies in projects like the `SharedKernel` and the core `Domain`, I think its acceptable to use something as lightweight as `MediatR.Contracts`

#### Usage
```csharp
public class SomethingHappened : DomainEvent
```

## Entity
An object defined primarily by its identity and represents a core concept within our domain, is an Entity. All AggregateRoots are Entities, but not all entities are an AggregateRoot.

An AggregateRoot is the consistency boundary that encapsulates all entities within a bounded context.

### Usage
```csharp
public class Example : Entity<ExampleId>
```


## StronglyTypedId
This is an opinionated pattern that is not necessarily central to Domain-Driven Design, but aligns with it well.

The concept is that it helps preventing accidental mixing of different IDs and provides a declarative way of passing around ID's in code.

#### Usage
```csharp
public class ExampleId : StronglyTypedId<ExampleId>
```

## ValueObject
Unlike Entities, ValueObjects are concepts that are defined by their attributes, not their identity, and are generally immutable and follow structural equality.

One of many good reason to use ValueObjects is to combat [Primitive Obsession](https://refactoring.guru/smells/primitive-obsession) - the practice of using primitive data types (`string`, `int`, `decimal`, `bool`, etc.) to represent concepts that have a more complex meaning and rules in our domain.

#### Usage
```csharp
public class Primitive : ValueObject
{
    public string Value { get; init; }
    public string Another { get; init; }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
        yield return Another;
    }
}
```

## DomainException
Represents errors that occur during domain logic execution, that are typically (invariants) business rule violations or invalid state transitions.

Similar to `Primitive Obsession`, having strongly typed exceptions, leads to more declarative code that is self-explanatory and easier to debug.

> Some people prefer a [Railway-Oriented Programming](https://fsharpforfunandprofit.com/rop/) approach when it comes to declerative error handlign [re: the Result Pattern](https://andrewlock.net/working-with-the-result-pattern-part-4-is-the-result-pattern-worth-it/), but I will stick to using Strongly-Typed Exceptions.

## Types
Within the [Types directory](Types/) we define *truly shared* primitives/valueobjects that are independent of any specific business subdomain and are stable and change resistant. 

An example of this is the [Money](Types/Money.cs) class, its a concept that is more complex than a simple `decimal`: Enforces non-negativity, currency consistency, and equality semantics

But as I pointed out earlier that the `SharedKernel` is not a dumping ground and we don't just throw around things that we think are shared because they might not be as this would lead to a dangerous pattern, due to:

**Coupling:** If we needded to change or add something to a ValueObject, only because one context needs it but the others don't, this violates the [YAGNI](https://martinfowler.com/bliki/Yagni.html) principle (see my explanation for the [Odometer](../../README.md#Bounded%20Context))

---

## IRepository
An age-old question on whether to use repositories or not, and whether they belong in the domain layer or the application layer, and "isn't it just an abstraction around the DbContext"? No. Its not.

Like all other patterns, if you use them in the wrong place, at the wrong time, they are counter productive/intuitive.

The repository pattern is not merely a wrapper around the DbContext, it is in fact an abstraction that requires the loading of the whole aggregate in order to maintain consistency.

One of the main reasons that devs have debated about this is because they don't take into account [CQRS](https://martinfowler.com/bliki/CQRS.html). The domain side of an application (Aggregates, etc.) deal with the Command-side of requests; the mutations, and a repository is meant to retrieve data for mutation - not just any data but rather to ensure that the Aggregate is being loaded as a whole.

Queries are an entirely different paradigm and require a specific way of thinking, however, queries are not (for the most part) relevant to the Domain model, which is mainly designed to exceute actions/commands/mutations and raise events.