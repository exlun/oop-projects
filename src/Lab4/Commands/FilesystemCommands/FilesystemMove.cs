namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FilesystemCommands;

public class FilesystemMove(Uri source, Uri destination) : FilesystemCommand
{
    public override void Execute()
    {
        if (Filesystem != null)
        {
            Filesystem.Move(source, destination);
        }
        else
        {
            throw new InvalidOperationException("Connection required.");
        }
    }
}