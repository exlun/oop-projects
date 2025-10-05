using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Users;

public class MessageState(Message message, bool isRead = false)
{
    public Message Message { get; init; } = message;

    public bool IsRead { get; private set; } = isRead;

    public bool MarkAsRead()
    {
        if (IsRead)
        {
            return false;
        }

        IsRead = true;
        return true;
    }
}