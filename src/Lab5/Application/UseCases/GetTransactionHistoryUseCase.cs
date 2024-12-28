using ApplicationInterfaces;

namespace UseCases;

public class GetTransactionHistoryUseCase
{
    private readonly ITransactionService _transactionService;

    public GetTransactionHistoryUseCase(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public DTOs.GetHistoryResponse Execute(DTOs.GetHistoryRequest request)
    {
        return _transactionService.GetHistory(request);
    }
}