using ValueObjects;

namespace DTOs;

public abstract record GetBalanceResult
{
    public sealed record FailResult(bool Failure, string Message) : GetBalanceResult;

    public sealed record SuccessResult(Money Money) : GetBalanceResult;
}