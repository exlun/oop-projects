using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class BasicLabworkBuilder : ILabworkBuilder
{
    public Guid Author { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Points Points { get; set; } = new(0);

    public string RatingCriteria { get; set; } = string.Empty;

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
