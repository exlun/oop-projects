namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public interface IIdentifiable : IEquatable<IIdentifiable>
{
    public Guid Id { get; }
}