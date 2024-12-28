using Entities;
using ValueObjects;
using Xunit;

namespace Lab5.Tests;

public class TransactionTests
{
    [Fact]
    public void CreateTransaction_WithValidParameters_ShouldInitializeCorrectly()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var amount = new Money(200);
        TransactionType type = TransactionType.Deposit;

        // Act
        var transaction = new Transaction(accountNumber, amount, type);

        // Assert
        Assert.Equal(accountNumber, transaction.AccountNumber);
        Assert.Equal(amount, transaction.Amount);
        Assert.Equal(type, transaction.Type);
        Assert.True(transaction.Id != Guid.Empty);
        Assert.True((DateTime.UtcNow - transaction.Timestamp).TotalSeconds < 5); // Проверка близости времени
    }
}