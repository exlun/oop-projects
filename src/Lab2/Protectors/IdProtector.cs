namespace Itmo.ObjectOrientedProgramming.Lab2.Protectors;

public class IdProtector<T>(Guid allowedAcessorId, T initialValue) : Protector<T>(initialValue)
{
    private Guid AllowedAcessorId { get; } = allowedAcessorId;

    public override bool IsWriteAccessAllowed(Guid accessorId)
    {
        return accessorId == AllowedAcessorId;
    }

    public override bool IsReadAccessAllowed(Guid accessorId)
    {
        return true;
    }
}
