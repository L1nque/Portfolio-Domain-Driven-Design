using System.Globalization;
using System.Text.RegularExpressions;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class Nationality : ValueObject
{
    public string Code { get; }
    public string Name { get; }

    private Nationality(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public static Nationality FromCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentNullException(nameof(code), "Nationality code cannot be null or empty");
        }


        if (!Regex.IsMatch(code.Trim(), @"^[A-Z]{2}$"))
        {
            throw new ArgumentException("Nationality code must be two alphabetic characters.", nameof(code));
        }


        var regionInfo = new RegionInfo(code.Trim());
        return new Nationality(code.Trim().ToUpper(), regionInfo.EnglishName);
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Code;
    }
}