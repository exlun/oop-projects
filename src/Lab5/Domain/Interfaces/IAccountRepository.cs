using Entities;
using ValueObjects;

namespace Interfaces;

public interface IAccountRepository
{
    Account? GetByAccountNumber(AccountNumber accountNumber);

    void Add(Account account);

    IEnumerable<Account> GetAll();

    void Update(Account account);
}