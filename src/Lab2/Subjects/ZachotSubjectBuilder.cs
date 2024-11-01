using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ZachotSubjectBuilder : SubjectBuilder
{
    public override Subject BuildSubject()
    {
        var newSubj = new ZachotSubject(AuthorId, Name, ZachotPoints, LabworksStorage, null);

        newSubj.LecturesStorage.Write(AuthorId, LecturesStorage);

        int labPoints = LabworksStorage.Sum(lab => lab.Points.Value);

        if (labPoints != newSubj.PointsSum.Value)
        {
            throw new InvalidDataException();
        }

        return newSubj;
    }

    public Points ZachotPoints { get; set; } = new(0);
}
