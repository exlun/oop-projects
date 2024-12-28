using ApplicationInterfaces;
using DTOs;

namespace UseCases;

public class DepositMoneyUseCase
{
    private readonly ITransactionService _transactionService;

    public DepositMoneyUseCase(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public DepositResponse Execute(DepositRequest request)
    {
        return _transactionService.Deposit(request);
    }
}