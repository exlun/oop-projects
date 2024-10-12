using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.Segments;
using Itmo.ObjectOrientedProgramming.Lab1.Simulation;

namespace Lab1.Tests;

public class TrainSimulatorTests
{
    [Fact]
    public void Train_NormalPath_CompletesRoute()
    {
        var train = new Train(10000, 10, 100);
        var powerRails = new PowerRails(10, 100);
        var defaultRails = new DefaultRails(10);
        var route = new Route([powerRails, defaultRails], 100);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        Assert.IsType<SimulationResult.SuccessResult>(result);
    }

    [Fact]
    public void Train_ExceededSpeed_ReturnsFailure()
    {
        var train = new Train(100, 10, 1000);
        var powerRails = new PowerRails(10, 1000);
        var defaultRails = new DefaultRails(10);
        var route = new Route([powerRails, defaultRails], 10);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        Assert.IsType<SimulationResult.FailResult>(result);
        var failResult = result as SimulationResult.FailResult;
        Assert.IsType<SegmentResultType.SegmentFailure.ExceededSpeed>(failResult?.Result);
    }

    [Fact]
    public void Train_ExceededPower_ReturnsFailure()
    {
        var train = new Train(100, 10, 100);
        var powerRails = new PowerRails(10, 1000);
        var defaultRails = new DefaultRails(10);
        var route = new Route([powerRails, defaultRails], 10);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        var failResult = result as SimulationResult.FailResult;
        Assert.IsType<SegmentResultType.SegmentFailure.ExceededPower>(failResult?.Result);
    }

    [Fact]
    public void Train_PathWithStation_CompletesRoute()
    {
        var train = new Train(100, 10, 100);
        var powerRails = new PowerRails(10, 10);
        var defaultRails = new DefaultRails(10);
        var station = new Station(5, 5, 50);
        var route = new Route([powerRails, defaultRails, station, defaultRails], 100);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        Assert.IsType<SimulationResult.SuccessResult>(result);
    }

    [Fact]
    public void Train_ExceededStationSpeedLimit_ReturnsFailure()
    {
        var train = new Train(100, 10, 100);
        var powerRails = new PowerRails(10, 100);
        var defaultRails = new DefaultRails(10);
        var station = new Station(5, 5, 5);
        var route = new Route([powerRails, station, defaultRails], 1000);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        var failResult = result as SimulationResult.FailResult;
        Assert.IsType<SegmentResultType.SegmentFailure.ExceededSpeed>(failResult?.Result);
    }

    [Fact]
    public void Train_ExceededSRouteSpeedLimitWhileNotExceedingStationSpeedLimit_ReturnsFailure()
    {
        var train = new Train(100, 10, 1000);
        var powerRails = new PowerRails(10, 1000);
        var defaultRails = new DefaultRails(10);
        var station = new Station(5, 5, 1000);
        var route = new Route([powerRails, defaultRails, station, defaultRails], 10);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        var failResult = result as SimulationResult.FailResult;
        Assert.IsType<SegmentResultType.SegmentFailure.ExceededSpeed>(failResult?.Result);
    }

    [Fact]
    public void Train_PathWithAccelerationAndDeceleration_CompletesRoute()
    {
        var train = new Train(100, 10, 100);
        var powerRailsAccelerating = new PowerRails(10, 100);
        var powerRailsDecelerating = new PowerRails(10, -80);
        var defaultRails = new DefaultRails(10);
        var station = new Station(5, 5, 100);
        var route = new Route([powerRailsAccelerating, defaultRails, powerRailsDecelerating, station, defaultRails, powerRailsAccelerating, defaultRails, powerRailsDecelerating], 100);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        Assert.IsType<SimulationResult.SuccessResult>(result);
    }

    [Fact]
    public void Train_PathWithNoAcceleration_ReturnsFailure()
    {
        var train = new Train(100, 10, 100);
        var defaultRails = new DefaultRails(10);
        var route = new Route([defaultRails], 100);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        var failResult = result as SimulationResult.FailResult;
        Assert.IsType<SegmentResultType.SegmentFailure.NotMoving>(failResult?.Result);
    }

    [Fact]
    public void Train_PathWithDecelerationExceedingAcceleration_ReturnsFailure()
    {
        var train = new Train(100, 10, 100);
        var powerRailsAccelerating = new PowerRails(10, 100);
        var powerRailsDecelerating = new PowerRails(10, -200);
        var route = new Route([powerRailsAccelerating, powerRailsDecelerating], 100);
        var simulator = new Simulator(train, route);
        SimulationResult result = simulator.TryCompletion();
        var failResult = result as SimulationResult.FailResult;
        Assert.IsType<SegmentResultType.SegmentFailure.WrongDirection>(failResult?.Result);
    }
}