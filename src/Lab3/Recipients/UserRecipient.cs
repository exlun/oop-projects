using Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Users;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class UserRecipient(User user) : IRecipient
{
    public void ReceiveMessage(Message message)
    {
        user.ReceiveMessage(message);
    }
}