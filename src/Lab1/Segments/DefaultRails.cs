namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class DefaultRails(double length) : ISegment
{
    public double Length { get; init; } = length;

    public double CompletionTime { get; private set; }

    public FailureType? TryCompletion(Models.Train train)
    {
        if (train.Speed < 0)
        {
            return new FailureType.WrongDirection();
        }

        if (train.Speed == 0)
        {
            return new FailureType.NotMoving();
        }

        double completedLength = 0;
        while (completedLength < Length)
        {
            completedLength += train.Speed * train.Accuracy;
            CompletionTime += train.Accuracy;
        }

        return null;
    }
}