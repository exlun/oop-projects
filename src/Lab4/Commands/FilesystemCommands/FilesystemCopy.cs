namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FilesystemCommands;

public class FilesystemCopy(Uri source, Uri destination) : FilesystemCommand
{
    public override void Execute()
    {
        if (Filesystem != null)
        {
            Filesystem.Copy(source, destination);
        }
        else
        {
            throw new InvalidOperationException("Connection required.");
        }
    }
}