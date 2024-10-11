using Itmo.ObjectOrientedProgramming.Lab1.Segments;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Route
{
    public IReadOnlyList<ISegment> Path { get; init; }

    public Route(Collection<ISegment> segments, double maxEndingSpeed)
    {
        var path = new Collection<ISegment>(segments);

        var pathEnd = new Station(0, 0, maxEndingSpeed);

        path.Add(pathEnd);
        Path = path;
    }
}