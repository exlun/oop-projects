using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public abstract class SubjectBuilder
{
    protected string Name { get; set; } = string.Empty;

    protected Collection<Labwork> LabworksStorage { get; init; } = [];

    protected Collection<Lecture> LecturesStorage { get; init; } = [];

    protected Guid AuthorId { get; set; }

    public abstract Subject BuildSubject();
}
