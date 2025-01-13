using ApplicationInterfaces;
using DTOs;
using Entities;
using Interfaces;
using Moq;
using Services;
using ValueObjects;
using Xunit;
using static Moq.Times;

namespace Lab5.Tests;

public class AccountServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _userServiceMock = new Mock<IUserService>();
        _accountService = new AccountService(_accountRepositoryMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public void CreateAccount_WithValidRequest_ShouldCreateAccountSuccessfully()
    {
        // Arrange
        var request = new CreateAccountRequest(new AccountNumber("1234567890"), new Pin("1234"), new Money(500));

        _accountRepositoryMock.Setup(repo => repo.GetByAccountNumber(request.AccountNumber))
            .Returns((Account?)null);

        // Act
        CreateAccountResponse response = _accountService.CreateAccount(request);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Account created successfully.", response.Message);
        _accountRepositoryMock.Verify(
            repo => repo.Add(It.Is<Account>(
            a => a.AccountNumber.Value == request.AccountNumber.Value &&
                 a.Pin == request.Pin &&
                 a.Balance == request.InitialDeposit)),
            Once);
    }

    [Fact]
    public void CreateAccount_WithExistingAccount_ShouldReturnFailure()
    {
        // Arrange
        var request = new CreateAccountRequest(new AccountNumber("1234567890"), new Pin("1234"), new Money(500));

        var existingAccount = new Account(request.AccountNumber, new Pin("0000"), new Money(100));
        _accountRepositoryMock.Setup(repo => repo.GetByAccountNumber(request.AccountNumber))
            .Returns(existingAccount);

        // Act
        CreateAccountResponse response = _accountService.CreateAccount(request);

        // Assert
        Assert.False(response.Success);
        Assert.Equal("Account already exists.", response.Message);
        _accountRepositoryMock.Verify(repo => repo.Add(It.IsAny<Account>()), Never);
    }

    [Fact]
    public void GetBalance_WithValidCredentials_ShouldReturnBalance()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var pin = new Pin("1234");

        var account = new Account(accountNumber, pin, new Money(1000));
        _userServiceMock.Setup(us => us.Login(accountNumber, pin))
            .Returns(new LoginResult(true, "Login successful."));

        _accountRepositoryMock.Setup(repo => repo.GetByAccountNumber(accountNumber))
            .Returns(account);

        // Act
        BalanceResponse response = _accountService.GetBalance(accountNumber, pin);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Balance retrieved successfully.", response.Message);
        Assert.Equal(new Money(1000), response.Balance);
    }

    [Fact]
    public void GetBalance_WithInvalidLogin_ShouldReturnFailure()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var pin = new Pin("0000");

        _userServiceMock.Setup(us => us.Login(accountNumber, pin))
            .Returns(new LoginResult(false, "Invalid PIN."));

        // Act
        BalanceResponse response = _accountService.GetBalance(accountNumber, pin);

        // Assert
        Assert.False(response.Success);
        Assert.Equal("Invalid PIN.", response.Message);
        Assert.Equal(new Money(0), response.Balance);
        _accountRepositoryMock.Verify(repo => repo.GetByAccountNumber(It.IsAny<AccountNumber>()), Never);
    }

    [Fact]
    public void GetBalance_WithNonExistingAccount_ShouldReturnFailure()
    {
        // Arrange
        var accountNumber = new AccountNumber("1234567890");
        var pin = new Pin("1234");

        _userServiceMock.Setup(us => us.Login(accountNumber, pin))
            .Returns(new LoginResult(true, "Login successful."));

        _accountRepositoryMock.Setup(repo => repo.GetByAccountNumber(accountNumber))
            .Returns((Account?)null);

        // Act
        BalanceResponse response = _accountService.GetBalance(accountNumber, pin);

        // Assert
        Assert.False(response.Success);
        Assert.Equal("Account not found.", response.Message);
        Assert.Equal(new Money(0), response.Balance);
    }
}