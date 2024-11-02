using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Courses;

public abstract class Course(string name, Guid head) : IIdentifiable
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = name;

    public Dictionary<Semester, Subject> Subjects { get; init; } = [];

    public Guid Head { get; init; } = head;

    public bool Equals(IIdentifiable? other)
    {
        return other != null && Id == other.Id;
    }
}
