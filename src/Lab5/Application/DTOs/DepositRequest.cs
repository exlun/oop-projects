using ValueObjects;

namespace DTOs;

public record DepositRequest(AccountNumber AccountNumber, Pin Pin, Money Amount);