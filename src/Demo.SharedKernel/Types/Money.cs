using Demo.SharedKernel.Constants;
using Demo.SharedKernel.Core.Models;

namespace Demo.SharedKernel.Types;

/// <summary>
/// Represents an amount of money in a specific currency.
/// This is a value object ensuring immutability and structural equality.
/// </summary>
public class Money : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> class.
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currency">The ISO 4217 currency code (e.g., "USD", "EUR").</param>
    /// <exception cref="ArgumentException">Thrown if amount is negative or currency is invalid.</exception>
    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency) || !CurrencyCodes.IsValid(currency))
            throw new ArgumentException($"Invalid currency code: {currency}", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant(); // Normalize currency code
    }

    /// <summary>
    /// Gets the monetary amount.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the ISO 4217 currency code.
    /// </summary>
    public string Currency { get; }

    /// <summary>
    /// Gets the atomic values for equality comparison.
    /// </summary>
    /// <returns>An enumerable containing the amount and currency.</returns>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }

    /// <summary>
    /// Adds two Money instances if they have the same currency.
    /// </summary>
    /// <param name="other">The other Money instance to add.</param>
    /// <returns>A new Money instance representing the sum.</returns>
    /// <exception cref="InvalidOperationException">Thrown if currencies do not match.</exception>
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add amounts in different currencies: {Currency} and {other.Currency}.");

        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// Subtracts one Money instance from another if they have the same currency.
    /// </summary>
    /// <param name="other">The other Money instance to subtract.</param>
    /// <returns>A new Money instance representing the difference.</returns>
    /// <exception cref="InvalidOperationException">Thrown if currencies do not match or if the result would be negative.</exception>
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot subtract amounts in different currencies: {Currency} and {other.Currency}.");

        var resultAmount = Amount - other.Amount;
        if (resultAmount < 0)
            throw new InvalidOperationException("Resulting amount cannot be negative.");

        return new Money(resultAmount, Currency);
    }

    /// <summary>
    /// Multiplies the Money amount by a factor.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>A new Money instance with the multiplied amount.</returns>
    public Money Multiply(decimal factor)
    {
        return new Money(Amount * factor, Currency);
    }

    /// <summary>
    /// Returns a string representation of the Money value.
    /// </summary>
    /// <returns>A string in the format "Amount Currency" (e.g., "100.50 USD").</returns>
    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }

    #region Static Factory Methods / Constants

    /// <summary>
    /// Gets a Money instance representing zero amount in the specified currency.
    /// </summary>
    /// <param name="currency">The currency code.</param>
    /// <returns>A Money instance with zero amount.</returns>
    public static Money Zero(string currency) => new(0, currency);

    #endregion
}