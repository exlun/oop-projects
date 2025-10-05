using Itmo.ObjectOrientedProgramming.Lab3.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class MessageFilterProxy(IRecipient recipient, int importanceLevel) : IRecipient
{
    public void ReceiveMessage(Message message)
    {
        if (FilterMessage(message))
        {
            recipient.ReceiveMessage(message);
        }
    }

    private bool FilterMessage(Message message)
    {
        return message.Importance >= importanceLevel;
    }
}