using DTOs;
using ValueObjects;

namespace ApplicationInterfaces;

public interface IUserService
{
    LoginResult Login(AccountNumber accountNumber, Pin pin);
}