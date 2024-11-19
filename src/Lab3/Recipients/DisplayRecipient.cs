using Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Displays;
using Itmo.ObjectOrientedProgramming.Lab3.Messages;

namespace Itmo.ObjectOrientedProgramming.Lab3.Recipients;

public class DisplayRecipient(IDisplayDriver displayDriver, IMessageColorist colorist) : IRecipient
{
    private readonly IDisplayDriver _displayDriver = displayDriver;

    private readonly IMessageColorist _colorist = colorist;

    public void ReceiveMessage(Message message)
    {
        _displayDriver.DisplayMessage(message.ToString(), _colorist.GetColor(message));
    }
}