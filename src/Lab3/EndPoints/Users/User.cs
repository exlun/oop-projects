using Itmo.ObjectOrientedProgramming.Lab3.Messages;
using Itmo.ObjectOrientedProgramming.Lab3.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Users;

public class User(string name) : IIdentifiable
{
    public string Name { get; init; } = name;

    public Guid Id { get; } = Guid.NewGuid();

    private List<MessageState> Messages { get; set; } = [];

    public bool Equals(IIdentifiable? other)
    {
        return other != null && other.Id == Id;
    }

    public void ReceiveMessage(Message message)
    {
        Messages.Add(new MessageState(message));
    }

    public bool ReadMessage(Message message)
    {
        MessageState? messageState = Messages.Find(messageState => messageState.Message.Id == message.Id);

        return messageState != null && messageState.MarkAsRead();
    }

    public MessageCheckResult CheckMessageStatus(Message message)
    {
        MessageState? messageState = Messages.Find(messageState => messageState.Message.Id == message.Id);
        if (messageState == null)
        {
            return new MessageCheckResult.MessageNotFound();
        }

        return new MessageCheckResult.MessageFound(messageState);
    }
}