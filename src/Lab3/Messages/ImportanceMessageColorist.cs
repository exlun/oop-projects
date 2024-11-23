using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class ImportanceMessageColorist : IMessageColorist
{
    public Color GetColor(Message message)
    {
        return message.Importance switch
        {
            < 10 => Color.Gray,
            < 20 => Color.White,
            _ => Color.Red,
        };
    }
}