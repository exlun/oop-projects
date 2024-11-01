using Itmo.ObjectOrientedProgramming.Lab2.Repository;
using System.Security;

namespace Itmo.ObjectOrientedProgramming.Lab2.Protectors;

public abstract class ProtectedList<T>(Guid authorId, ICollection<T> initial) where T : IIdentifiable
{
    protected Guid AuthorId { get; } = authorId;

    protected IList<T> Storage { get; init; } = initial.ToList();

    public IReadOnlyList<T> AsReadOnly()
    {
        return Storage.AsReadOnly();
    }

    public abstract bool IsWriteAccessAllowed(Guid accessorId);

    public void Write(Guid accessorId, IReadOnlyCollection<T> values)
    {
        if (IsWriteAccessAllowed(accessorId))
        {
            foreach (T item in values)
            {
                Storage.Add(item);
            }
        }
        else
        {
            throw new SecurityException();
        }
    }

    public void Add(Guid accessorId, T item)
    {
        if (IsWriteAccessAllowed(accessorId))
        {
            Storage.Add(item);
        }
        else
        {
            throw new SecurityException();
        }
    }

    public bool Remove(Guid accessorId, Guid itemId)
    {
        if (IsWriteAccessAllowed(accessorId))
        {
            return (from item in Storage where item.Id == itemId select Storage.Remove(item)).FirstOrDefault();
        }
        else
        {
            throw new SecurityException();
        }
    }

    public abstract bool IsReadAccessAllowed(Guid accessorId);
}
