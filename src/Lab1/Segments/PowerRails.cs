namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class PowerRails(double length, double power) : ISegment
{
    public double Length { get; init; } = length;

    public double CompletionTime { get; private set; }

    public double Power { get; init; } = power;

    public FailureType? TryCompletion(Models.Train train)
    {
        double completedLength = 0;
        while (completedLength < Length)
        {
            if (!train.TryApplyPower(Power))
            {
                return new FailureType.ExceededPower();
            }

            completedLength += train.Speed * train.Accuracy;
            CompletionTime += train.Accuracy;

            if (CheckForSpeed(train) is not null)
            {
                return CheckForSpeed(train);
            }
        }

        return null;
    }

    private FailureType? CheckForSpeed(Models.Train train)
    {
        if (train.Speed < 0)
        {
            return new FailureType.WrongDirection();
        }

        if (train.Speed == 0)
        {
            return new FailureType.NotMoving();
        }

        return null;
    }
}