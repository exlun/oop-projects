using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public abstract class User(string name) : IIdentifiable
{
    public string Name { get; init; } = name;

    public Guid Id { get; } = Guid.NewGuid();

    public bool Equals(IIdentifiable? other)
    {
        return other != null && other.Id == Id;
    }
}