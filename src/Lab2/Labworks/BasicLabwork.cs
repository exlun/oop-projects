using Itmo.ObjectOrientedProgramming.Lab2.ValueTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class BasicLabwork(
                       Guid authorId,
                       string name,
                       string description,
                       Points points,
                       string ratingCriteria,
                       Guid? sourceLabworkId) :

                       Labwork(authorId,
                               name,
                               description,
                               points,
                               ratingCriteria,
                               sourceLabworkId)
{
    public override Labwork Clone()
    {
        var labworkClone = new BasicLabwork(
                                           AuthorId,
                                           new string(Name.GetValue(AuthorId)),
                                           new string(Description.GetValue(AuthorId)),
                                           Points,
                                           new string(RatingCriteria.GetValue(AuthorId)),
                                           Id);

        return labworkClone;
    }
}
