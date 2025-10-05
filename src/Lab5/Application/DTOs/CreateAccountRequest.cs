using ValueObjects;

namespace DTOs;

public record CreateAccountRequest(AccountNumber AccountNumber, Pin Pin, Money InitialDeposit);