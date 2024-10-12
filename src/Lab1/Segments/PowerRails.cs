namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class PowerRails(double length, double power) : ISegment
{
    public double Length { get; init; } = length;

    public double Power { get; init; } = power;

    public SegmentResultType TryComplete(Models.Train train)
    {
        double completionTime = 0;
        double completedLength = 0;
        while (completedLength < Length)
        {
            if (!train.TryApplyPower(Power))
            {
                return new SegmentResultType.SegmentFailure.ExceededPower();
            }

            completedLength += train.Speed * train.Accuracy;
            completionTime += train.Accuracy;

            SegmentResultType.SegmentFailure? speedFailureResult = CheckForSpeedFailures(train);
            if (speedFailureResult is not null)
            {
                return speedFailureResult;
            }
        }

        return new SegmentResultType.SegmentSuccess(completionTime);
    }

    private SegmentResultType.SegmentFailure? CheckForSpeedFailures(Models.Train train)
    {
        if (train.Speed < 0)
        {
            return new SegmentResultType.SegmentFailure.WrongDirection();
        }

        if (train.Speed == 0)
        {
            return new SegmentResultType.SegmentFailure.NotMoving();
        }

        return null;
    }
}