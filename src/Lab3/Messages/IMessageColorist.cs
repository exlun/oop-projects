using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public interface IMessageColorist
{
    Color GetColor(Message message);
}