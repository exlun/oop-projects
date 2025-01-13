using ValueObjects;

namespace DTOs;

public record TransactionDto(
    Guid Id,
    AccountNumber AccountNumber,
    Money Amount,
    DateTime Timestamp,
    TransactionType Type);

public enum TransactionType
{
    Deposit,
    Withdrawal,
}