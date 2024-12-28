using ValueObjects;

namespace DTOs;

public record WithdrawRequest(AccountNumber AccountNumber, Pin Pin, Money Amount);