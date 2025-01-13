using ApplicationInterfaces;
using DTOs;
using Entities;
using ValueObjects;

namespace Services;

public class AccountService(Interfaces.IAccountRepository accountRepository, IUserService userService)
    : IAccountService
{
    public CreateAccountResponse CreateAccount(CreateAccountRequest request)
    {
        AccountNumber accountNumber = request.AccountNumber;
        var pin = new Pin(request.Pin.Value);
        var initialDeposit = new Money(request.InitialDeposit.Amount);
        Account? existingAccount = accountRepository.GetByAccountNumber(accountNumber);
        if (existingAccount != null)
        {
            return new CreateAccountResponse(false, "Account already exists.");
        }

        var account = new Account(accountNumber, pin, initialDeposit);
        accountRepository.Add(account);

        return new CreateAccountResponse(true, "Account created successfully.");
    }

    public BalanceResponse GetBalance(AccountNumber accountNumber, Pin pin)
    {
        LoginResult loginResult = userService.Login(accountNumber, pin);
        if (!loginResult.Success)
        {
            return new BalanceResponse(false, loginResult.Message, new Money(0));
        }

        Account? account = accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
        {
            return new BalanceResponse(false, "Account not found.", new Money(0));
        }

        return new BalanceResponse(true, "Balance retrieved successfully.", account.Balance);
    }
}