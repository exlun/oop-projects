using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Segments;

namespace Itmo.ObjectOrientedProgramming.Lab1.Simulation;

public class Simulator(Models.Train train, Route route)
{
    public Train SimulationTrain { get; init; } = train;

    public Route SimulationRoute { get; init; } = route;

    public SimulationResult TryCompletion()
    {
        double pathTime = 0;
        foreach (ISegment segment in SimulationRoute.Path)
        {
            FailureType? completionResult = segment.TryComplete(SimulationTrain);
            if (completionResult != null)
            {
                return new SimulationResult.FailResult(completionResult);
            }

            pathTime += segment.CompletionTime;
        }

        return new SimulationResult.SuccessResult(pathTime);
    }
}