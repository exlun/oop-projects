using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Messengers;

public class ConsoleMessenger(IConsole console) : IMessenger
{
    private readonly IConsole _console = console;

    public void ReceiveMessage(Message message)
    {
        _console.WriteLine($"Messenger: {message}");
    }
}