using Itmo.ObjectOrientedProgramming.Lab3.Utils;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Displays;

public class ConsoleDisplay(IConsole console) : IDisplay
{
    private readonly IConsole _console = console;

    private string _message = string.Empty;

    private Color _currentColor = Color.White;

    public void Clear()
    {
        _message = string.Empty;
    }

    public void DisplayMessage()
    {
        _console.WriteLine(Crayon.Output.Rgb(_currentColor.R, _currentColor.G, _currentColor.B).Text($"ON DISPLAY: {_message}"));
    }

    public void SetMessage(string message)
    {
        if (_message.Length == 0)
        {
            _message = message;
        }
        else
        {
            throw new InvalidOperationException("Display is already in use");
        }
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
    }
}