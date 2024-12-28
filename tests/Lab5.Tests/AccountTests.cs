using Entities;
using ValueObjects;
using Xunit;

namespace Lab5.Tests;

public class AccountTests
{
    [Fact]
    public void CreateAccount_WithValidParameters_ShouldInitializeCorrectly()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var pin = new Pin("1234");
        var initialBalance = new Money(1000);

        // Act
        var account = new Account(accountNumber, pin, initialBalance);

        // Assert
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(pin, account.Pin);
        Assert.Equal(initialBalance, account.Balance);
    }

    [Fact]
    public void Deposit_WithValidAmount_ShouldIncreaseBalance()
    {
        var accountNumber = new AccountNumber("1234567890");

        // Arrange
        var account = new Account(accountNumber, new Pin("1234"), new Money(500));
        var depositAmount = new Money(200);

        // Act
        account.Deposit(depositAmount);

        // Assert
        Assert.Equal(new Money(700), account.Balance);
    }

    [Fact]
    public void Withdraw_WithValidAmount_ShouldDecreaseBalance()
    {
        var accountNumber = new AccountNumber("1234567890");

        // Arrange
        var account = new Account(accountNumber, new Pin("1234"), new Money(500));
        var withdrawAmount = new Money(200);

        // Act
        account.Withdraw(withdrawAmount);

        // Assert
        Assert.Equal(new Money(300), account.Balance);
    }

    [Fact]
    public void Withdraw_WithAmountExceedingBalance_ShouldThrowInvalidOperationException()
    {
        var accountNumber = new AccountNumber("1234567890");

        // Arrange
        var account = new Account(accountNumber, new Pin("1234"), new Money(500));
        var withdrawAmount = new Money(600);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(withdrawAmount));
    }

    [Fact]
    public void ValidatePin_WithCorrectPin_ShouldReturnTrue()
    {
        var accountNumber = new AccountNumber("1234567890");

        // Arrange
        var account = new Account(accountNumber, new Pin("1234"), new Money(500));

        // Act
        bool isValid = account.ValidatePin(new Pin("1234"));

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void ValidatePin_WithIncorrectPin_ShouldReturnFalse()
    {
        var accountNumber = new AccountNumber("1234567890");

        // Arrange
        var account = new Account(accountNumber, new Pin("1234"), new Money(500));

        // Act
        bool isValid = account.ValidatePin(new Pin("0000"));

        // Assert
        Assert.False(isValid);
    }
}