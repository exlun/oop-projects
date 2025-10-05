namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Directories.DirectoryIterator;

public interface IDIrectoryIterator
{
    IEnumerable<IDataUnit> GetSubItems(Directory root, int depth);
}