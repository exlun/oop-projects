using System.Text.RegularExpressions;

namespace ValueObjects;

public class AccountNumber : IEquatable<AccountNumber>
{
    private static readonly Regex AccountNumberRegex = new(@"^\d{10}$");

    public string Value { get; }

    public AccountNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Account number cannot be empty.", nameof(value));

        if (!AccountNumberRegex.IsMatch(value))
            throw new ArgumentException("Account number must be exactly 10 digits.", nameof(value));

        Value = value;
    }

    public bool Equals(AccountNumber? other)
    {
        if (other is null)
            return false;

        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is AccountNumber other && Equals(other);
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(Value, StringComparison.CurrentCulture);
    }

    public static bool operator ==(AccountNumber left, AccountNumber right) => left.Equals(right);

    public static bool operator !=(AccountNumber left, AccountNumber right) => !left.Equals(right);
}