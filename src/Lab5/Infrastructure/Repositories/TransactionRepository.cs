using Data;
using Entities;
using Interfaces;
using Npgsql;
using ValueObjects;

namespace Repositories;

public class TransactionRepository(IDatabaseConnectionFactory connectionFactory) : ITransactionRepository
{
    public void Add(Transaction transaction)
    {
        const string command = @"
                INSERT INTO transactions (id, account_number, amount, timestamp, type)
                VALUES (@Id, @AccountNumber, @Amount, @Timestamp, @Type);
            ";

        using NpgsqlConnection connection = connectionFactory.CreateConnection();
        using var cmd = new NpgsqlCommand(command, connection);
        cmd.Parameters.AddWithValue("Id", transaction.Id);
        cmd.Parameters.AddWithValue("AccountNumber", transaction.AccountNumber.Value);
        cmd.Parameters.AddWithValue("Amount", transaction.Amount.Amount);
        cmd.Parameters.AddWithValue("Timestamp", transaction.Timestamp);
        cmd.Parameters.AddWithValue("Type", transaction.Type.ToString().ToLower(System.Globalization.CultureInfo.CurrentCulture));

        cmd.ExecuteNonQuery();
    }

    public IEnumerable<Transaction> GetTransactionsForAccount(AccountNumber accountNumber)
    {
        const string query = @"
                SELECT id, account_number, amount, timestamp, type
                FROM transactions
                WHERE account_number = @AccountNumber
                ORDER BY timestamp DESC;
            ";

        var transactions = new List<Transaction>();

        using NpgsqlConnection connection = connectionFactory.CreateConnection();
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("AccountNumber", accountNumber.Value);

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Guid id = reader.GetGuid(0);
            var accNumber = new AccountNumber(reader.GetString(1));
            var amount = new Money(reader.GetInt32(2));
            DateTime timestamp = reader.GetDateTime(3);
            string typeStr = reader.GetString(4);
            TransactionType type = typeStr.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
            {
                "deposit" => TransactionType.Deposit,
                "withdrawal" => TransactionType.Withdrawal,
                _ => throw new InvalidOperationException($"Unknown transaction type: {typeStr}"),
            };

            var transaction = new Transaction(accNumber, amount, type);
            typeof(Transaction).GetProperty(nameof(Transaction.Id))?.SetValue(transaction, id);
            typeof(Transaction).GetProperty(nameof(Transaction.Timestamp))?.SetValue(transaction, timestamp);

            transactions.Add(transaction);
        }

        return transactions;
    }
}