using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ZachotSubjectBuilder : SubjectBuilder
{
    private Points ZachotPoints { get; set; } = new(0);

    public ZachotSubjectBuilder WithAuthorId(Guid authorId)
    {
        AuthorId = authorId;
        return this;
    }

    public ZachotSubjectBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public ZachotSubjectBuilder WithZachotPoints(Points zachotPoints)
    {
        ZachotPoints = zachotPoints;
        return this;
    }

    public ZachotSubjectBuilder WithLabworksStorage(Collection<Labwork> labworksStorage)
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

    public ZachotSubjectBuilder WithLecturesStorage(Collection<Lecture> lecturesStorage)
    {
        foreach (Lecture lecture in lecturesStorage.Select(x =>
                 {
                     LecturesStorage.Add(x);
                     return x;
                 }))
        {
            Lecture lecture1 = lecture;
        }

        return this;
    }

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
}
