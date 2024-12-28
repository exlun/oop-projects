using Entities;

namespace DTOs;

public abstract record GetTransactionHistoryResult()
{
    public sealed record FailResult(bool Failure, string Message) : GetTransactionHistoryResult;

    public sealed record SuccessResult(IEnumerable<Transaction> Transactions) : GetTransactionHistoryResult;
}