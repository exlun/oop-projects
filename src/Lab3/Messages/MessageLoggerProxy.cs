using Itmo.ObjectOrientedProgramming.Lab3.Loggers;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class MessageLoggerProxy(IRecipient recipient, IMessageLogger messageLogger) : IRecipient
{
    public void ReceiveMessage(Message message)
    {
        messageLogger.Log(message);
        recipient.ReceiveMessage(message);
    }
}