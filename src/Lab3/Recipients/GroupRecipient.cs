using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class GroupRecipient() : IRecipient
{
    private readonly List<IRecipient> _recipients = [];

    public void AddRecipient(IRecipient recipient)
    {
        _recipients.Add(recipient);
    }

    public void RemoveRecipient(IRecipient recipient)
    {
        _recipients.Remove(recipient);
    }

    public void AddRange(IEnumerable<IRecipient> recipients)
    {
        _recipients.AddRange(recipients);
    }

    public void ReceiveMessage(Message message)
    {
        foreach (IRecipient recipient in _recipients)
        {
            recipient.ReceiveMessage(message);
        }
    }
}