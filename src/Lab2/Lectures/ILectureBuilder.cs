namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public interface ILectureBuilder
{
    string Name { get; set; }

    string Description { get; set; }

    string Content { get; set; }

    Guid AuthorId { get; set; }

    Guid? SourceId { get; set; }

    Lecture BuildLecture();
}
