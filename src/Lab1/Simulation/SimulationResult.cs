using Itmo.ObjectOrientedProgramming.Lab1.Segments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Simulation;

public class SimulationResult
{
    public class FailResult(FailureType failure) : SimulationResult
    {
        public FailureType Result { get; init; } = failure;
    }

    public class SuccessResult(double time) : SimulationResult
    {
        public double CompletionTime { get; init; } = time;
    }
}