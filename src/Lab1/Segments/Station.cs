namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class Station(double load, double length, double maxSpeed) : ISegment
{
    public double Length { get; init; } = length;

    public double CompletionTime { get; private set; } = load;

    public double MaxSpeed { get; init; } = maxSpeed;

    public FailureType? TryComplete(Models.Train train)
    {
        if (train.Speed > MaxSpeed)
        {
            return new FailureType.ExceededSpeed();
        }

        return null;
    }
}