namespace Itmo.ObjectOrientedProgramming.Lab1.Segments;

public interface ISegment
{
    public double Length { get; init; }

    public double CompletionTime { get; }

    public FailureType? TryComplete(Models.Train train);
}