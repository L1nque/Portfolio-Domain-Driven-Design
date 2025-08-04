using Demo.Domain.FleetManagement.Exceptions;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// VO encapsulating a VIN. Its a good example of why primitive obsession is bad
/// as there can be "rich" domain behavior.
/// 
/// In this case the vin string has parts that represent different things.
/// </summary>
public class Vin : ValueObject
{
    /// <summary>
    /// these characters universally not allowed in VINs
    /// </summary>
    public static readonly char[] InvalidVinCharacters = { 'I', 'O', 'Q' };

    public string Number { get; init; }

    /// <summary>
    /// First three digits: World Manufacturer Identifier
    /// </summary>
    public string WMI => Number[..3];

    /// <summary>
    /// Next six digits: Vehicle Descriptor Section
    /// </summary>
    public string VDS => Number[3..9];

    /// <summary>
    /// Last eight digits: Vehicle Identifier Section 
    /// </summary>
    public string VIS => Number[^8..];

    public Vin(string number)
    {
        Validate(number);
        Number = number.ToUpper();
    }

    /// <summary>
    /// Universally 17 characters long and do not contain <see cref="InvalidVinCharacters"/>
    /// </summary>
    /// <param name="number"></param>
    /// <exception cref="InvalidVinException"></exception>
    public void Validate(string number)
    {
        if (number.Length != 17)
            throw new InvalidVinException(number.Length);

        if (number.ToUpper().All(c => !char.IsLetterOrDigit(c) || InvalidVinCharacters.Contains(char.ToUpper(c))))
            throw new InvalidVinException($"Invalid characters detected: {number}");
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Number;
    }
}
