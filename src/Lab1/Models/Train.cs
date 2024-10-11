namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Train(double mass, double accuracy, double maxPower)
{
    public double Mass { get; init; } = mass;

    public double Speed { get; private set; }

    public double Acceleration { get; private set; }

    public double MaxPower { get; init; } = maxPower;

    public double Accuracy { get; init; } = accuracy;

    public bool TryApplyPower(double power)
    {
        if (power > MaxPower)
        {
            return false;
        }

        Acceleration = power / Mass;
        RecalculateSpeed();
        return true;
    }

    private void RecalculateSpeed()
    {
        Speed += Acceleration * Accuracy;
    }
}
