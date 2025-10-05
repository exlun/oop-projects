using ValueObjects;

namespace DTOs;

public record BalanceResponse(bool Success, string Message, Money Balance);