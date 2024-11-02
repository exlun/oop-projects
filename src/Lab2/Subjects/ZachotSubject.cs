using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ZachotSubject(Guid authorId, string name, Points points, IList<Labwork> labworks, Guid? sourceId)
    : Subject(authorId, name, labworks, sourceId)
{
    public Points PointsNeeded { get; init; } = points;

    public override Subject Clone()
    {
        var newSubj = new ZachotSubject(
            AuthorId,
            new string(Name.GetValue(AuthorId)),
            PointsNeeded,
            labworks,
            Id);

        newSubj.LecturesStorage.Write(AuthorId, LecturesStorage.AsReadOnly());

        return newSubj;
    }

    public override bool Equals(IIdentifiable? other)
    {
        return other != null && Id == other.Id;
    }
}