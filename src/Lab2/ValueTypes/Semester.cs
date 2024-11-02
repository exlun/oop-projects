namespace Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

public class Semester : IEquatable<Semester>
{
    public Semester(int semester)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(semester);
        Value = semester;
    }

    public int Value { get; private set; }

    public bool Equals(Semester? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Semester)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}