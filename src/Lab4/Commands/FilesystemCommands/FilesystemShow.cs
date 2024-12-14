namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FilesystemCommands;

public class FilesystemShow(Uri target, Stream outStream) : FilesystemCommand
{
    public override void Execute()
    {
        if (Filesystem != null)
        {
            using Stream inStream = Filesystem.OpenRead(target);
            inStream.CopyTo(outStream);
            inStream.Close();
        }
        else
        {
            throw new InvalidOperationException("Connection required.");
        }
    }
}