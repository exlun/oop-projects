namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public interface ISegment
{
    public double Length { get; init; }

    public SegmentResultType TryComplete(Models.Train train);
}