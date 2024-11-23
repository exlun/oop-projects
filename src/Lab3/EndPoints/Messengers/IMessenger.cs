using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Messengers;

public interface IMessenger
{
    void ReceiveMessage(Message message);
}