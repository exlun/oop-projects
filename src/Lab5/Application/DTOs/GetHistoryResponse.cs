namespace DTOs;

public record GetHistoryResponse(bool Success, string Message, IEnumerable<TransactionDto>? History);