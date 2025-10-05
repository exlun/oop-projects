using Entities;
using Interfaces;
using Moq;
using Services;
using ValueObjects;
using Xunit;

namespace Lab5.Tests;

public class UserServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _userService = new UserService(_accountRepositoryMock.Object);
    }

    [Fact]
    public void Login_WithValidCredentials_ShouldReturnSuccess()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var pin = new Pin("1234");

        var account = new Account(accountNumber, pin, new Money(500));
        _accountRepositoryMock.Setup(repo => repo.GetByAccountNumber(accountNumber))
            .Returns(account);

        // Act
        DTOs.LoginResult result = _userService.Login(accountNumber, pin);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Login successful.", result.Message);
    }

    [Fact]
    public void Login_WithInvalidPin_ShouldReturnFailure()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var pin = new Pin("0000");

        var account = new Account(accountNumber, new Pin("1234"), new Money(500));
        _accountRepositoryMock.Setup(repo => repo.GetByAccountNumber(accountNumber))
            .Returns(account);

        // Act
        DTOs.LoginResult result = _userService.Login(accountNumber, pin);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Invalid PIN.", result.Message);
    }

    [Fact]
    public void Login_WithNonExistingAccount_ShouldReturnFailure()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var pin = new Pin("1234");

        _accountRepositoryMock.Setup(repo => repo.GetByAccountNumber(accountNumber))
            .Returns((Account?)null);

        // Act
        DTOs.LoginResult result = _userService.Login(accountNumber, pin);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Account not found.", result.Message);
    }
}