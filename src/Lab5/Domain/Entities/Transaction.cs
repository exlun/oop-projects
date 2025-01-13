using ValueObjects;

namespace Entities;

public class Transaction(AccountNumber accountNumber, Money amount, TransactionType type)
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public AccountNumber AccountNumber { get; private set; } = accountNumber;

    public Money Amount { get; private set; } = amount;

    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

    public TransactionType Type { get; private set; } = type;
}

public enum TransactionType
{
    Deposit,
    Withdrawal,
}