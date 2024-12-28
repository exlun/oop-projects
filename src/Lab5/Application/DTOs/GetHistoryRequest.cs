using ValueObjects;

namespace DTOs;

public record GetHistoryRequest(AccountNumber AccountNumber, Pin Pin);