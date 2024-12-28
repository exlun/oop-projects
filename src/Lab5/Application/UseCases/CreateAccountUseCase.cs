using ApplicationInterfaces;
using DTOs;

namespace UseCases;

public class CreateAccountUseCase
{
    private readonly IAccountService _accountService;

    public CreateAccountUseCase(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public CreateAccountResponse Execute(CreateAccountRequest request)
    {
        return _accountService.CreateAccount(request);
    }
}