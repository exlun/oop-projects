using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ExamSubjectBuilder : SubjectBuilder
{
    private Points ExamPoints { get; set; } = new(0);

    public ExamSubjectBuilder WithAuthorId(Guid authorId)
    {
        AuthorId = authorId;
        return this;
    }

    public ExamSubjectBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public ExamSubjectBuilder WithExamPoints(Points examPoints)
    {
        ExamPoints = examPoints;
        return this;
    }

    public ExamSubjectBuilder WithLabworksStorage(Collection<Labwork> labworksStorage)
    {
        foreach (Labwork labwork in labworksStorage.Select(x =>
                 {
                     LabworksStorage.Add(x);
                     return x;
                 }))
        {
            Labwork labwork1 = labwork;
        }

        return this;
    }

    public ExamSubjectBuilder WithLecturesStorage(Collection<Lecture> lecturesStorage)
    {
        lecturesStorage.Select(x =>
        {
            this.LecturesStorage.Add(x);
            return x;
        });
        return this;
    }

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
}
