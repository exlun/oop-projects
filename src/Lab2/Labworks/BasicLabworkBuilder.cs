using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class BasicLabworkBuilder
{
    private Guid Author { get; set; }

    private string Name { get; set; } = string.Empty;

    private string Description { get; set; } = string.Empty;

    private Points Points { get; set; } = new(0);

    private string RatingCriteria { get; set; } = string.Empty;

    public BasicLabworkBuilder WithAuthor(Guid author)
    {
        Author = author;
        return this;
    }

    public BasicLabworkBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public BasicLabworkBuilder WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public BasicLabworkBuilder WithPoints(Points points)
    {
        Points = points;
        return this;
    }

    public BasicLabworkBuilder WithRatingCriteria(string ratingCriteria)
    {
        RatingCriteria = ratingCriteria;
        return this;
    }

    public Labwork BuildLabwork()
    {
        return new BasicLabwork(
            Author,
            new string(Name),
            new string(Description),
            Points,
            new string(RatingCriteria),
            null);
    }
}