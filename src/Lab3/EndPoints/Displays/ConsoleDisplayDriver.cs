using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Displays;

public class ConsoleDisplayDriver(ConsoleDisplay display) : IDisplayDriver
{
    private readonly ConsoleDisplay _display = display;

    public void ClearDisplay()
    {
        _display.Clear();
    }

    public void DisplayMessage(string message, Color color)
    {
        _display.Clear();
        _display.SetColor(color);
        _display.SetMessage(message);
        _display.DisplayMessage();
    }
}