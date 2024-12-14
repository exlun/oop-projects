namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FilesystemCommands;

public class FilesystemDelete(Uri target) : FilesystemCommand
{
    public override void Execute()
    {
        if (Filesystem != null)
        {
            Filesystem.Delete(target);
        }
        else
        {
            throw new InvalidOperationException("Connection required.");
        }
    }
}