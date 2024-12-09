namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Directories;

public sealed class LocalDirectory : Directories.Directory
{
    public override Uri Uri { get; }

    public override IList<Directory> SubDirectories { get; init; } = new List<Directory>();

    public override IList<Files.File> Files { get; init; } = new List<Files.File>();

    public LocalDirectory(string absolutePath)
    {
        Uri = new Uri(absolutePath);

        if (!System.IO.Directory.Exists(Uri.LocalPath))
        {
            throw new DirectoryNotFoundException();
        }

        var dirs = System.IO.Directory.GetDirectories(Uri.LocalPath).Select(dir => new LocalDirectory(dir)).ToList();
        var files = System.IO.Directory.GetFiles(Uri.LocalPath).Select(file => new Files.LocalFile(file)).ToList();

        foreach (LocalDirectory dir in dirs)
        {
            SubDirectories.Add(dir);
        }

        foreach (Files.LocalFile file in files)
        {
            Files.Add(file);
        }
    }
}