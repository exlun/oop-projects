using ApplicationInterfaces;
using DTOs;
using ValueObjects;

namespace UseCases;

public class CheckBalanceUseCase(IAccountService accountService)
{
    public BalanceResponse Execute(AccountNumber accountNumber, Pin pin)
    {
        return accountService.GetBalance(accountNumber, pin);
    }
}