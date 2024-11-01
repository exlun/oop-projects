using Itmo.ObjectOrientedProgramming.Lab2.Protectors;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public abstract class Labwork(
    Guid authorId,
    string name,
    string description,
    Points points,
    string ratingCriteria,
    Guid? sourceLabworkId) : IIdentifiable
{
    public Guid Id { get; } = Guid.NewGuid();

    public IdProtector<string> Name { get; init; } = new(authorId, name);

    public IdProtector<string> Description { get; init; } = new(authorId, description);

    public IdProtector<string> RatingCriteria { get; init; } = new(authorId, ratingCriteria);

    public Points Points { get; init; } = points;

    public Guid AuthorId { get; init; } = authorId;

    public Guid? SourceLabworkId { get; init; } = sourceLabworkId;

    public abstract Labwork Clone();

    public bool Equals(IIdentifiable? other)
    {
        return other != null && Id == other.Id;
    }
}