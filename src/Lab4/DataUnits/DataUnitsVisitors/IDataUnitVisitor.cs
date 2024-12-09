namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;

public interface IDataUnitVisitor
{
    void VisitDirectory(Directories.Directory directory);

    void VisitFile(Files.File file);
}