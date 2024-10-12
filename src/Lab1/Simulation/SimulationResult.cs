using Itmo.ObjectOrientedProgramming.Lab1.Segments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Simulation;

public abstract record SimulationResult
{
    public sealed record FailResult(SegmentResultType SegmentResult) : SimulationResult
    {
        public SegmentResultType Result => SegmentResult;
    }

    public sealed record SuccessResult(double Time) : SimulationResult
    {
        public double CompletionTime => Time;
    }
}