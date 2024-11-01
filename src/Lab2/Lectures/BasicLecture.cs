namespace Itmo.ObjectOrientedProgramming.Lab2.Lectures;

public class BasicLecture(
    Guid authorId,
    string name,
    string description,
    string content,
    Guid? sourceId) :

    Lecture(authorId,
        name,
        description,
        content,
        sourceId)
{
    public override Lecture Clone()
    {
        var clone = new BasicLecture(
                                     AuthorId,
                                     new string(Name.GetValue(AuthorId)),
                                     new string(Description.GetValue(AuthorId)),
                                     new string(Content.GetValue(AuthorId)),
                                     Id);

        return clone;
    }
}