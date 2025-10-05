using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Protectors;

public class IdProtectedList<T>(Guid authorId, Collection<T> initial) : ProtectedList<T>(authorId, initial)
    where T : IIdentifiable
{
    public override bool IsWriteAccessAllowed(Guid accessorId)
    {
        return accessorId == AuthorId;
    }

    public override bool IsReadAccessAllowed(Guid accessorId)
    {
        return true;
    }
}
