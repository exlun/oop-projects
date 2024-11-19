using Itmo.ObjectOrientedProgramming.Lab3.Utils;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public record Message(string Header, string Body, int Importance) : IIdentifiable
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public override string ToString()
    {
        return $"{Header}\n{Body}";
    }

    public virtual bool Equals(IIdentifiable? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }
}