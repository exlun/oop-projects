namespace Itmo.ObjectOrientedProgramming.Lab3.Utils;

public interface IIdentifiable : IEquatable<IIdentifiable>
{
    Guid Id { get; }
}