using ApplicationInterfaces;
using DomainServices;
using DTOs;
using Entities;
using ValueObjects;

namespace Services;

public class TransactionService(ATMService atmService) : ITransactionService
{
    public DepositResponse Deposit(DepositRequest request)
    {
        var amount = new Money(request.Amount.Amount);
        atmService.Deposit(request.AccountNumber, request.Pin, amount);

        return new DepositResponse(true, "Deposit successful.");
    }

    public WithdrawResponse Withdraw(WithdrawRequest request)
    {
        var amount = new Money(request.Amount.Amount);
        atmService.Withdraw(request.AccountNumber, request.Pin, amount);

        return new WithdrawResponse(true, "Withdrawal successful.");
    }

    public GetHistoryResponse GetHistory(GetHistoryRequest request)
    {
        GetTransactionHistoryResult result = atmService.GetTransactionHistory(request.AccountNumber, request.Pin);
        if (result is GetTransactionHistoryResult.FailResult failResult)
        {
            return new GetHistoryResponse(false, failResult.Message, null);
        }

        IEnumerable<Transaction>? history = (result as GetTransactionHistoryResult.SuccessResult)?.Transactions;
        if (history != null)
        {
            var historyDto = history.Select(h => new TransactionDto(
                h.Id,
                h.AccountNumber,
                h.Amount,
                h.Timestamp,
                (DTOs.TransactionType)h.Type)).ToList();

            return new GetHistoryResponse(true, "History retrieved successfully.", historyDto);
        }

        return new GetHistoryResponse(false, "History not retrieved.", null);
    }
}