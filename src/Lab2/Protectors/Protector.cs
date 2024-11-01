using System.Security;

namespace Itmo.ObjectOrientedProgramming.Lab2.Protectors;

public abstract class Protector<T>(T initialValue)
{
    protected T ProtectedValue { get; set; } = initialValue;

    public abstract bool IsWriteAccessAllowed(Guid accessorId);

    public abstract bool IsReadAccessAllowed(Guid accessorId);

    public T GetValue(Guid accessorId)
    {
        if (IsReadAccessAllowed(accessorId))
        {
            return ProtectedValue;
        }
        else
        {
            throw new SecurityException();
        }
    }

    public void SetValue(Guid accessorId, T value)
    {
        if (IsWriteAccessAllowed(accessorId))
        {
            ProtectedValue = value;
        }
        else
        {
            throw new SecurityException();
        }
    }
}