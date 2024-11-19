using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Utils;
using System.Reflection;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Users;

public class User(string name) : IIdentifiable
{
    public string Name { get; init; } = name;

    public Guid Id { get; } = Guid.NewGuid();

    public bool Equals(IIdentifiable? other)
    {
        return other != null && other.Id == Id;
    }

    private List<MessageState> Messages { get; set; } = [];

    public void ReceiveMessage(Message message)
    {
        Messages.Add(new MessageState(message));
    }

    public void ReadMessage(Message message)
    {
        if (Messages == null)
        {
            throw new TargetException("Message not found");
        }

        foreach (MessageState messageState in Messages.Where(messageState => messageState.Message.Id == message.Id))
        {
            messageState.MarkAsRead();
            return;
        }

        throw new TargetException("Message not found");
    }

    public bool CheckMessageStatus(Message message)
    {
        if (Messages == null)
        {
            throw new TargetException("Message not found");
        }

        foreach (MessageState messageState in Messages.Where(messageState => messageState.Message.Id == message.Id))
        {
            return messageState.IsRead;
        }

        throw new TargetException("Message not found");
    }
}