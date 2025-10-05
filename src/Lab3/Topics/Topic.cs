using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Recipients;

namespace Itmo.ObjectOrientedProgramming.Lab3.Topics;

public class Topic(string name, IReadOnlyList<IRecipient> recipients)
{
    public string Name { get; init; } = name;

    private IReadOnlyList<IRecipient> Recipients { get; } = recipients;

    public void SendMessage(Message message)
    {
        foreach (IRecipient recipient in Recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }
}