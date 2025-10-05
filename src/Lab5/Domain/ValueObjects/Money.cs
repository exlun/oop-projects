namespace ValueObjects;

public struct Money : IEquatable<Money>
{
    public int Amount { get; private set; } = 0;

    public Money(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        Amount = amount;
    }

    public bool Equals(Money other)
    {
        return Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        return obj is Money other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Amount.GetHashCode();
    }

    public static bool operator ==(Money left, Money right) => left.Equals(right);

    public static bool operator !=(Money left, Money right) => !left.Equals(right);

    public static Money operator +(Money money, int amount) => new Money(money.Amount + amount);

    public static Money operator -(Money money, int amount) => new Money(money.Amount - amount);

    public static bool operator <(Money money, int amount) => money.Amount < amount;

    public static bool operator >(Money money, int amount) => money.Amount > amount;
}