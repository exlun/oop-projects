using ApplicationInterfaces;
using DTOs;

namespace UseCases;

public class WithdrawMoneyUseCase
{
    private readonly ITransactionService _transactionService;

    public WithdrawMoneyUseCase(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public WithdrawResponse Execute(WithdrawRequest request)
    {
        return _transactionService.Withdraw(request);
    }
}