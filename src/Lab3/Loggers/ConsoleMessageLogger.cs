using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public class ConsoleMessageLogger : IMessageLogger
{
    public void Log(Message message)
    {
        Console.WriteLine($"ID: {{message.Id}} {{{message.Importance}}} [{DateTime.Now:HH:mm:ss}] \n" +
                          $"[{message.Header}] {message.Body} ");
    }
}