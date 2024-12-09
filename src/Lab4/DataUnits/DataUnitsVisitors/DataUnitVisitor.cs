namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;

public class DataUnitVisitor : IDataUnitVisitor
{
    public Directories.Directory? Directory { get; private set; }

    public Files.File? File { get; private set; }

    public void VisitDirectory(Directories.Directory directory)
    {
        Directory = directory;
        File = null;
    }

    public void VisitFile(Files.File file)
    {
        File = file;
        Directory = null;
    }
}