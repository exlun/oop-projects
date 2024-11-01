using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public abstract class SubjectBuilder
{
    public abstract Subject BuildSubject();

    public string Name { get; set; } = string.Empty;

    public Collection<Labwork> LabworksStorage { get; init; } = [];

    public Collection<Lecture> LecturesStorage { get; init; } = [];

    public Guid AuthorId { get; set; }
}
