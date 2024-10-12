namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class DefaultRails(double length) : ISegment
{
    public double Length { get; init; } = length;

    public SegmentResultType TryComplete(Models.Train train)
    {
        if (train.Speed < 0)
        {
            return new SegmentResultType.SegmentFailure.WrongDirection();
        }

        if (train.Speed == 0)
        {
            return new SegmentResultType.SegmentFailure.NotMoving();
        }

        double completionTime = 0;
        double completedLength = 0;
        while (completedLength < Length)
        {
            completedLength += train.Speed * train.Accuracy;
            completionTime += train.Accuracy;
        }

        return new SegmentResultType.SegmentSuccess(completionTime);
    }
}