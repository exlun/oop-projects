using DTOs;
using ValueObjects;

namespace ApplicationInterfaces;

public interface IAccountService
{
    CreateAccountResponse CreateAccount(CreateAccountRequest request);

    BalanceResponse GetBalance(AccountNumber accountNumber, Pin pin);
}