namespace Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

public class Points
{
    public Points(int points)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(points);
        Value = points;
    }

    public int Value { get; private set; }
}
