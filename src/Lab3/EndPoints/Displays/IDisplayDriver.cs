using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.EndPoints.Displays;

public interface IDisplayDriver
{
    void ClearDisplay();

    void DisplayMessage(string message, Color color);
}