using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Users;

public class MessageState(Message message, bool isRead = false)
{
    public Message Message { get; init; } = message;

    public bool IsRead { get; private set; } = isRead;

    public void MarkAsRead()
    {
        if (IsRead)
        {
            throw new InvalidOperationException("Message is already read");
        }

        IsRead = true;
    }
}