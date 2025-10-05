using System.Text.RegularExpressions;

namespace ValueObjects;

public class Pin : IEquatable<Pin>
{
    private static readonly Regex PinRegex = new(@"^\d{4}$");

    public string Value { get; }

    public Pin(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("PIN cannot be empty.", nameof(value));

        if (!PinRegex.IsMatch(value))
            throw new ArgumentException("PIN must be exactly 4 digits.", nameof(value));

        Value = value;
    }

    public bool Equals(Pin? other)
    {
        if (other is null)
            return false;

        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Pin other && Equals(other);
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(Value, StringComparison.CurrentCulture);
    }

    public static bool operator ==(Pin left, Pin right) => left.Equals(right);

    public static bool operator !=(Pin left, Pin right) => !left.Equals(right);
}