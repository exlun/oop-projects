using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Displays;

public class FileDisplay(string filename) : IDisplay
{
    private readonly string _filename = filename;

    private string _message = string.Empty;

    private Color _currentColor = Color.White;

    public void Clear()
    {
        _message = string.Empty;
    }

    public void DisplayMessage()
    {
        File.WriteAllText(_filename, $"<span style=\"color: rgb({_currentColor.R} {_currentColor.G} {_currentColor.B});\">{_message}</span><br>");
    }

    public bool SetMessage(string message)
    {
        if (_message.Length != 0) return false;
        _message = message;
        return true;
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
    }
}