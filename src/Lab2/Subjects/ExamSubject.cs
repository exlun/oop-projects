using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ExamSubject(Guid authorId, string name, Points examPoints, IList<Labwork> labworks, Guid? sourceId) : Subject(authorId, name, labworks, sourceId)
{
    public Points ExamPoints { get; init; } = examPoints;

    public override Subject Clone()
    {
        var newSubj = new ExamSubject(
                               AuthorId,
                               new string(Name.GetValue(AuthorId)),
                               ExamPoints,
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
