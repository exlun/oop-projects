using DTOs;

namespace ApplicationInterfaces;

public interface ITransactionService
{
    DepositResponse Deposit(DepositRequest request);

    WithdrawResponse Withdraw(WithdrawRequest request);

    GetHistoryResponse GetHistory(GetHistoryRequest request);
}