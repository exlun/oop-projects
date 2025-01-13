using Entities;
using ValueObjects;

namespace Interfaces;

public interface ITransactionRepository
{
    void Add(Transaction transaction);

    IEnumerable<Transaction> GetTransactionsForAccount(AccountNumber accountNumber);
}