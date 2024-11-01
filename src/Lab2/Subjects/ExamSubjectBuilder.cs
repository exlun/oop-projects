using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ExamSubjectBuilder : SubjectBuilder
{
    public override Subject BuildSubject()
    {
        var newSubj = new ExamSubject(AuthorId, Name, ExamPoints, LabworksStorage, null);

        newSubj.LecturesStorage.Write(AuthorId, LecturesStorage);

        int labPoints = LabworksStorage.Sum(lab => lab.Points.Value);

        if (labPoints + ExamPoints.Value != newSubj.PointsSum.Value)
        {
            throw new InvalidDataException();
        }

        return newSubj;
    }

    public Points ExamPoints { get; set; } = new(0);
}
