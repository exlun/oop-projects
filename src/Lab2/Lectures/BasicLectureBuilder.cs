namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public class BasicLectureBuilder
{
    private string Name { get; set; } = string.Empty;

    private string Description { get; set; } = string.Empty;

    private string Content { get; set; } = string.Empty;

    private Guid AuthorId { get; set; }

    public BasicLectureBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public BasicLectureBuilder WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public BasicLectureBuilder WithContent(string content)
    {
        Content = content;
        return this;
    }

    public BasicLectureBuilder WithAuthorId(Guid authorId)
    {
        AuthorId = authorId;
        return this;
    }

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