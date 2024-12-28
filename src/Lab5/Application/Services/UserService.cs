using ApplicationInterfaces;
using DTOs;
using ValueObjects;

namespace Services;

public class UserService(Interfaces.IAccountRepository accountRepository) : IUserService
{
    public LoginResult Login(AccountNumber accountNumber, Pin pin)
    {
        Entities.Account? account = accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
        {
            return new LoginResult(false, "Account not found.");
        }

        if (!account.ValidatePin(pin))
        {
            return new LoginResult(false, "Invalid PIN.");
        }

        return new LoginResult(true, "Login successful.");
    }
}