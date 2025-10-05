using ValueObjects;

namespace DTOs;

public record AccountDto(AccountNumber AccountNumber, Money Balance);