namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public class BasicLectureBuilder : ILectureBuilder
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public Guid AuthorId { get; set; }

    public Guid? SourceId { get; set; }

    public Lecture BuildLecture()
    {
        return new BasicLecture(
                                AuthorId,
                                new string(Name),
                                new string(Description),
                                new string(Content),
                                null);
    }
}
