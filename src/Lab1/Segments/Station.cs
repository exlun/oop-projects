namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public class Station(double load, double length, double maxSpeed) : ISegment
{
    public double Length { get; init; } = length;

    public double MaxSpeed { get; init; } = maxSpeed;

    public SegmentResultType TryComplete(Models.Train train)
    {
        if (train.Speed > MaxSpeed)
        {
            return new SegmentResultType.SegmentFailure.ExceededSpeed();
        }

        return new SegmentResultType.SegmentSuccess(load);
    }
}