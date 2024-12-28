using ValueObjects;

namespace Entities;

public class Account(AccountNumber accountNumber, Pin pin, Money initialBalance = default)
{
    public AccountNumber AccountNumber { get; private set; } = accountNumber;

    public Pin Pin { get; private set; } = pin;

    public Money Balance { get; private set; } = initialBalance;

    public void Deposit(Money amount)
    {
        Balance += amount.Amount;
    }

    public void Withdraw(Money amount)
    {
        if (Balance < amount.Amount)
            throw new InvalidOperationException("Insufficient funds.");

        Balance -= amount.Amount;
    }

    public bool ValidatePin(Pin pin)
    {
        return Pin == pin;
    }
}