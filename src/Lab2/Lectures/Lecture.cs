using Itmo.ObjectOrientedProgramming.Lab2.Protectors;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public abstract class Lecture(
    Guid authorId,
    string name,
    string description,
    string content,
    Guid? sourceId) : IIdentifiable
{
    public Guid Id { get; } = Guid.NewGuid();

    public IdProtector<string> Name { get; init; } = new(authorId, name);

    public IdProtector<string> Description { get; init; } = new(authorId, description);

    public IdProtector<string> Content { get; init; } = new(authorId, content);

    public Guid AuthorId { get; init; } = authorId;

    public Guid? SourceId { get; init; } = sourceId;

    public abstract Lecture Clone();

    public bool Equals(IIdentifiable? other)
    {
        return other != null && other.Id == Id;
    }
}