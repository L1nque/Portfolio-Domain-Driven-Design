using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.CustomerRelations.ValueObjects;

/// <summary>
/// Stores basic facts about the customer's engagmenet and (rentals) usage. If
/// we were to grow this into some sort of rich analytics domain with lots of metrics/insights,
/// then we might move it to be its own aggregate root.
/// 
/// More so, if "customer behavior tracking" grows with much more complex domain requirements,
/// such as loyalty programs, churn prediction, ML-based algorithms, etc., we can split it out
/// into its own BC for e.g. "Behavior Analysis".
/// </summary>
public class BehaviorProfile : ValueObject
{
    public BehaviorProfile(int totalRentals, Money totalSpent, float averageDrivingDistance)
    {
        TotalRentals = totalRentals;
        TotalSpent = totalSpent;
        AverageDrivingDistance = averageDrivingDistance;
    }

    public int TotalRentals { get; }
    public Money TotalSpent { get; }
    public float AverageDrivingDistance { get; }

    public void GetLoyaltyScore()
    {
        var averageSpend = TotalSpent.Amount / TotalRentals;

        if (TotalRentals > 5 && averageSpend > 1000)
        {
            // return LoyaltyScore.High;
        }

        // return LoyaltScore.Normal;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return TotalRentals;
        yield return TotalSpent;
        yield return AverageDrivingDistance;
    }
}
