using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;

namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Directories;

public abstract class Directory : IDataUnit
{
    public abstract Uri Uri { get; }

    public abstract IList<Directory> SubDirectories { get; init; }

    public abstract IList<Files.File> Files { get; init; }

    public void Accept(IDataUnitVisitor visitor)
    {
        visitor.VisitDirectory(this);
    }

    public int CompareTo(IDataUnit? other)
    {
        return other != null ? Uri.Compare(Uri, other.Uri, UriComponents.AbsoluteUri, UriFormat.Unescaped, StringComparison.CurrentCulture) : 0;
    }
}