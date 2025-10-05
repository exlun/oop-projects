using Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Messengers;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class MessengerRecipient(IMessenger messenger) : IRecipient
{
    public void ReceiveMessage(Message message)
    {
        messenger.ReceiveMessage(message);
    }
}