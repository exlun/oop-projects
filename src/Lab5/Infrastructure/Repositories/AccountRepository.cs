using Data;
using Entities;
using Interfaces;
using Npgsql;
using ValueObjects;

namespace Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IDatabaseConnectionFactory _connectionFactory;

    public AccountRepository(IDatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Account? GetByAccountNumber(AccountNumber accountNumber)
    {
        const string query = @"
                SELECT account_number, pin, balance
                FROM accounts
                WHERE account_number = @AccountNumber;
            ";

        using NpgsqlConnection connection = _connectionFactory.CreateConnection();
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("AccountNumber", accountNumber.Value);

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var accNumber = new AccountNumber(reader.GetString(0));
            var pin = new Pin(reader.GetString(1));
            var balance = new Money(reader.GetInt32(2));
            return new Account(accNumber, pin, balance);
        }

        return null;
    }

    public void Add(Account account)
    {
        const string command = @"
                INSERT INTO accounts (account_number, pin, balance)
                VALUES (@AccountNumber, @Pin, @Balance);
            ";

        using NpgsqlConnection connection = _connectionFactory.CreateConnection();
        using var cmd = new NpgsqlCommand(command, connection);
        cmd.Parameters.AddWithValue("AccountNumber", account.AccountNumber.Value);
        cmd.Parameters.AddWithValue("Pin", account.Pin.Value);
        cmd.Parameters.AddWithValue("Balance", account.Balance.Amount);

        cmd.ExecuteNonQuery();
    }

    public IEnumerable<Account> GetAll()
    {
        const string query = @"
                SELECT account_number, pin, balance
                FROM accounts;
            ";

        var accounts = new List<Account>();

        using NpgsqlConnection connection = _connectionFactory.CreateConnection();
        using var cmd = new NpgsqlCommand(query, connection);

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var accNumber = new AccountNumber(reader.GetString(0));
            var pin = new Pin(reader.GetString(1));
            var balance = new Money(reader.GetInt32(2));
            accounts.Add(new Account(accNumber, pin, balance));
        }

        return accounts;
    }

    public void Update(Account account)
    {
        const string command = @"
                UPDATE accounts
                SET pin = @Pin, balance = @Balance
                WHERE account_number = @AccountNumber;
            ";

        using NpgsqlConnection connection = _connectionFactory.CreateConnection();
        using var cmd = new NpgsqlCommand(command, connection);
        cmd.Parameters.AddWithValue("Pin", account.Pin.Value);
        cmd.Parameters.AddWithValue("Balance", account.Balance.Amount);
        cmd.Parameters.AddWithValue("AccountNumber", account.AccountNumber.Value);

        cmd.ExecuteNonQuery();
    }
}