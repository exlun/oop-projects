using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public interface ILabworkBuilder
{
    Guid Author { get; set; }

    string Name { get; set; }

    string Description { get; set; }

    Points Points { get; set; }

    string RatingCriteria { get; set; }

    Labwork BuildLabwork();
}
