using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Protectors;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public abstract class Subject(
    Guid authorId,
    string name,
    ICollection<Labwork> labworks,
    Guid? sourceId) : IIdentifiable
{
    public virtual Points PointsSum { get; init; } = new(100);

    public Guid Id { get; } = Guid.NewGuid();

    public IdProtector<string> Name { get; init; } = new(authorId, name);

    public Guid AuthorId { get; init; } = authorId;

    public Guid? SourceId { get; init; } = sourceId;

    public IdProtectedList<Lecture> LecturesStorage { get; init; } = new(authorId, []);

    public IReadOnlyList<Labwork> LabworksStorage { get; init; } = labworks.Select(labwork => labwork.Clone()).ToList();

    public abstract Subject Clone();

    public abstract bool Equals(IIdentifiable? other);
}