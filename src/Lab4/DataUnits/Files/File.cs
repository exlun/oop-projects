using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;

namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Files;

public abstract class File : IDataUnit
{
    public abstract Uri Uri { get; }

    public void Accept(IDataUnitVisitor visitor)
    {
        visitor.VisitFile(this);
    }

    public int CompareTo(IDataUnit? other)
    {
        return other != null ? Uri.Compare(Uri, other.Uri, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.CurrentCulture) : 0;
    }
}