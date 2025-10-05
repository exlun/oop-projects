using DTOs;
using Entities;
using Interfaces;
using ValueObjects;
using TransactionType = Entities.TransactionType;

namespace DomainServices;

public class ATMService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
{
    public virtual DepositResult Deposit(AccountNumber accountNumber, Pin pin, Money amount)
    {
        Account? account = accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
            return new DepositResult(false, "Account not found.");

        if (!account.Pin.Equals(pin))
            return new DepositResult(false, "Invalid PIN.");

        account.Deposit(amount);
        accountRepository.Update(account);

        var transaction = new Transaction(accountNumber, amount, TransactionType.Deposit);
        transactionRepository.Add(transaction);

        return new DepositResult(true, "Deposit successful");
    }

    public virtual DepositResult Withdraw(AccountNumber accountNumber, Pin pin, Money amount)
    {
        Account? account = accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
            return new DepositResult(false, "Account not found.");

        if (!account.Pin.Equals(pin))
            return new DepositResult(false, "Invalid PIN.");

        account.Withdraw(amount);
        accountRepository.Update(account);

        var transaction = new Transaction(accountNumber, amount, TransactionType.Withdrawal);
        transactionRepository.Add(transaction);

        return new DepositResult(true, "Withdraw successful");
    }

    public GetBalanceResult GetBalance(AccountNumber accountNumber, Pin pin)
    {
        Account? account = accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
            return new GetBalanceResult.FailResult(false, "Account not found.");

        if (!account.Pin.Equals(pin))
            return new GetBalanceResult.FailResult(false, "Invalid PIN.");

        return new GetBalanceResult.SuccessResult(account.Balance);
    }

    public GetTransactionHistoryResult GetTransactionHistory(AccountNumber accountNumber, Pin pin)
    {
        Account? account = accountRepository.GetByAccountNumber(accountNumber);
        if (account == null)
            return new GetTransactionHistoryResult.FailResult(false, "Account not found.");

        if (!account.Pin.Equals(pin))
            return new GetTransactionHistoryResult.FailResult(false, "Invalid PIN.");

        return new GetTransactionHistoryResult.SuccessResult(transactionRepository.GetTransactionsForAccount(accountNumber));
    }
}