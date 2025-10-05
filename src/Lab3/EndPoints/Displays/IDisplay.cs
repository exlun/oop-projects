using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Displays;

public interface IDisplay
{
    void Clear();

    void DisplayMessage();

    bool SetMessage(string message);

    void SetColor(Color color);
}