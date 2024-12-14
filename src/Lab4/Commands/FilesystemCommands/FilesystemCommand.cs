using Itmo.ObjectOrientedProgramming.Lab4.Filesystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FilesystemCommands;

public abstract class FilesystemCommand : ICommand
{
    public static IFilesystem? Filesystem { get; set; }

    public abstract void Execute();
}