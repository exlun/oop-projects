namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Files;

public sealed class LocalFile : File
{
    public override Uri Uri { get; }

    public Stream FileStream => new FileStream(Uri.LocalPath, FileMode.Open, FileAccess.ReadWrite);

    public LocalFile(string absolutePath)
    {
        Uri = new Uri(absolutePath);

        if (!System.IO.File.Exists(Uri.LocalPath) || !Uri.IsFile)
        {
            throw new FileNotFoundException();
        }
    }
}