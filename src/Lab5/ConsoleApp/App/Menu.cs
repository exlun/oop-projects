using DTOs;
using Helpers;
using UseCases;
using ValueObjects;

namespace ATM.ConsoleApp;

public class Menu
{
    private readonly CreateAccountUseCase _createAccountUseCase;
    private readonly DepositMoneyUseCase _depositMoneyUseCase;
    private readonly WithdrawMoneyUseCase _withdrawMoneyUseCase;
    private readonly CheckBalanceUseCase _checkBalanceUseCase;
    private readonly GetTransactionHistoryUseCase _getTransactionHistoryUseCase;

    public Menu(
        CreateAccountUseCase createAccountUseCase,
        DepositMoneyUseCase depositMoneyUseCase,
        WithdrawMoneyUseCase withdrawMoneyUseCase,
        CheckBalanceUseCase checkBalanceUseCase,
        GetTransactionHistoryUseCase getTransactionHistoryUseCase)
    {
        _createAccountUseCase = createAccountUseCase;
        _depositMoneyUseCase = depositMoneyUseCase;
        _withdrawMoneyUseCase = withdrawMoneyUseCase;
        _checkBalanceUseCase = checkBalanceUseCase;
        _getTransactionHistoryUseCase = getTransactionHistoryUseCase;
    }

    public void Display()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteLineInfo("=== Банкомат ===");
            Console.WriteLine("1. Создать счет");
            Console.WriteLine("2. Депозит средств");
            Console.WriteLine("3. Снять средства");
            Console.WriteLine("4. Проверить баланс");
            Console.WriteLine("5. Просмотреть историю операций");
            Console.WriteLine("6. Выход");
            Console.WriteLine();

            string choice = ConsoleHelper.ReadLine("Выберите действие (1-6): ");

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    DepositMoney();
                    break;
                case "3":
                    WithdrawMoney();
                    break;
                case "4":
                    CheckBalance();
                    break;
                case "5":
                    ViewTransactionHistory();
                    break;
                case "6":
                    ConsoleHelper.WriteLineInfo("Завершение работы...");
                    return;
                default:
                    ConsoleHelper.WriteLineError("Неверный выбор. Попробуйте снова.");
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void CreateAccount()
    {
        Console.Clear();
        ConsoleHelper.WriteLineInfo("=== Создание Счета ===");

        var accountNumber = new AccountNumber(ConsoleHelper.ReadLine("Введите номер счета (10 цифр): "));
        var pin = new Pin(ConsoleHelper.ReadLine("Введите PIN (4 цифры): "));
        string depositInput = ConsoleHelper.ReadLine("Введите начальный депозит: ");

        if (!int.TryParse(depositInput, out int initialDeposit) || initialDeposit < 0)
        {
            ConsoleHelper.WriteLineError("Некорректная сумма депозита.");
            ConsoleHelper.Pause();
            return;
        }

        var request = new CreateAccountRequest(accountNumber, pin, new Money(initialDeposit));

        try
        {
            CreateAccountResponse response = _createAccountUseCase.Execute(request);
            if (response.Success)
            {
                ConsoleHelper.WriteLineSuccess(response.Message);
            }
            else
            {
                ConsoleHelper.WriteLineError(response.Message);
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteLineError($"Ошибка: {ex.Message}");
        }

        ConsoleHelper.Pause();
    }

    private void DepositMoney()
    {
        Console.Clear();
        ConsoleHelper.WriteLineInfo("=== Депозит Средств ===");

        var accountNumber = new AccountNumber(ConsoleHelper.ReadLine("Введите номер счета: "));
        var pin = new Pin(ConsoleHelper.ReadLine("Введите PIN: "));
        string amountInput = ConsoleHelper.ReadLine("Введите сумму депозита: ");

        if (!int.TryParse(amountInput, out int amount) || amount <= 0)
        {
            ConsoleHelper.WriteLineError("Некорректная сумма депозита.");
            ConsoleHelper.Pause();
            return;
        }

        var request = new DepositRequest(accountNumber, pin, new Money(amount));

        try
        {
            DepositResponse response = _depositMoneyUseCase.Execute(request);
            if (response.Success)
            {
                ConsoleHelper.WriteLineSuccess(response.Message);
            }
            else
            {
                ConsoleHelper.WriteLineError(response.Message);
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteLineError($"Ошибка: {ex.Message}");
        }

        ConsoleHelper.Pause();
    }

    private void WithdrawMoney()
    {
        Console.Clear();
        ConsoleHelper.WriteLineInfo("=== Снятие Средств ===");

        var accountNumber = new AccountNumber(ConsoleHelper.ReadLine("Введите номер счета: "));
        var pin = new Pin(ConsoleHelper.ReadLine("Введите PIN: "));
        string amountInput = ConsoleHelper.ReadLine("Введите сумму для снятия: ");

        if (!int.TryParse(amountInput, out int amount) || amount <= 0)
        {
            ConsoleHelper.WriteLineError("Некорректная сумма для снятия.");
            ConsoleHelper.Pause();
            return;
        }

        var request = new WithdrawRequest(accountNumber, pin, new Money(amount));

        try
        {
            WithdrawResponse response = _withdrawMoneyUseCase.Execute(request);
            if (response.Success)
            {
                ConsoleHelper.WriteLineSuccess(response.Message);
            }
            else
            {
                ConsoleHelper.WriteLineError(response.Message);
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteLineError($"Ошибка: {ex.Message}");
        }

        ConsoleHelper.Pause();
    }

    private void CheckBalance()
    {
        Console.Clear();
        ConsoleHelper.WriteLineInfo("=== Проверка Баланса ===");

        var accountNumber = new AccountNumber(ConsoleHelper.ReadLine("Введите номер счета: "));
        var pin = new Pin(ConsoleHelper.ReadLine("Введите PIN: "));

        try
        {
            BalanceResponse response = _checkBalanceUseCase.Execute(accountNumber, pin);
            if (response.Success)
            {
                ConsoleHelper.WriteLineSuccess($"Текущий баланс: {response.Balance.Amount:C}");
            }
            else
            {
                ConsoleHelper.WriteLineError(response.Message);
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteLineError($"Ошибка: {ex.Message}");
        }

        ConsoleHelper.Pause();
    }

    private void ViewTransactionHistory()
    {
        Console.Clear();
        ConsoleHelper.WriteLineInfo("=== Просмотр Истории Операций ===");

        var accountNumber = new AccountNumber(ConsoleHelper.ReadLine("Введите номер счета: "));
        var pin = new Pin(ConsoleHelper.ReadLine("Введите PIN: "));

        try
        {
            GetHistoryResponse response = _getTransactionHistoryUseCase.Execute(new GetHistoryRequest(accountNumber, pin));
            if (response.Success)
            {
                if (response.History != null)
                {
                    foreach (TransactionDto entry in response.History)
                    {
                        Console.WriteLine($"{entry.Timestamp}: {entry.Type} - {entry.Amount.Amount}");
                    }
                }
            }
            else
            {
                ConsoleHelper.WriteLineError(response.Message);
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteLineError($"Ошибка: {ex.Message}");
        }

        ConsoleHelper.Pause();
    }
}