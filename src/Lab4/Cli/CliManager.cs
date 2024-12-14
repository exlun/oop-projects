using Itmo.ObjectOrientedProgramming.Lab4.Commands.FilesystemCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Filesystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Cli;

public static class CliManager
{
    internal static LocalFilesystem? Filesystem { get; set; }

    internal static Uri? Cwd { get; set; }

    internal static Stream OutStream { get; set; } = Console.OpenStandardOutput();

    internal static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            string[] commandParts = input.Split(' ');
            string command = commandParts[0].ToLower(System.Globalization.CultureInfo.CurrentCulture);

            try
            {
                switch (command)
                {
                    case "connect":
                        if (IsConnected())
                        {
                            HandleConnectCommand(commandParts);
                        }

                        break;
                    case "disconnect":
                        if (IsConnected())
                        {
                            HandleDisconnectCommand();
                        }

                        break;
                    case "tree":
                        if (IsConnected())
                        {
                            HandleTreeCommand(commandParts);
                        }

                        break;
                    case "file":
                        if (IsConnected())
                        {
                            HandleFileCommand(commandParts);
                        }

                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    internal static bool IsConnected()
    {
        if (Filesystem == null)
        {
            Console.WriteLine("No active connection.");
            return false;
        }

        return true;
    }

    internal static void HandleConnectCommand(string[] parts)
    {
        if (parts.Length < 2)
        {
            Console.WriteLine("Usage: connect [Address] [-m Mode]");
            return;
        }

        string address = parts[1];

        if (!address.EndsWith('/') || !address.EndsWith('\\'))
        {
            address = $"{address}/";
        }

        string mode = "local";

        if (parts.Length > 3 && parts[2] == "-m")
        {
            mode = parts[3];
        }
        else
        {
            Console.WriteLine("Usage: connect [Address] [-m Mode]");
            return;
        }

        if (mode == "local")
        {
            Filesystem = new LocalFilesystem();
            FilesystemCommand.Filesystem = Filesystem;
            Cwd = new Uri(address);
            Console.WriteLine("Connected to local filesystem.");
        }
        else
        {
            Console.WriteLine("Unsupported filesystem mode.");
        }
    }

    internal static void HandleDisconnectCommand()
    {
        Cwd = null;
        Filesystem = null;
        FilesystemCommand.Filesystem = null;
        Console.WriteLine("Disconnected from filesystem.");
    }

    internal static void HandleTreeCommand(string[] parts)
    {
        if (parts.Length < 2)
        {
            Console.WriteLine("Usage: tree goto [Path] | tree list {-d Depth}");
            return;
        }

        string subCommand = parts[1].ToLower(System.Globalization.CultureInfo.CurrentCulture);

        switch (subCommand)
        {
            case "goto":
                HandleTreeGotoCommand(parts);
                break;
            case "list":
                HandleTreeListCommand(parts);
                break;
            default:
                Console.WriteLine("Unknown tree command.");
                break;
        }
    }

    internal static void HandleTreeGotoCommand(string[] parts)
    {
        if (Cwd == null)
        {
            throw new InvalidOperationException();
        }

        if (parts.Length < 3)
        {
            Console.WriteLine("Usage: tree goto [Path]");
            return;
        }

        string path = parts[2];
        Uri? uri = Filesystem?.GetUriByPath(path, Cwd);
        if (uri != null)
        {
            Cwd = uri;
        }
        else
        {
            Console.WriteLine("Invalid path.");
        }
    }

    internal static void HandleTreeListCommand(string[] parts)
    {
        int depth = int.MaxValue;

        if (parts.Length > 3 && parts[2] == "-d")
        {
            if (!int.TryParse(parts[3], out depth))
            {
                Console.WriteLine("Usage: tree list {-d Depth}");
                return;
            }
        }

        if (Cwd != null)
        {
            FilesystemCommand.Filesystem = Filesystem;
            var command = new FilesystemList(Cwd, depth);
            command.Execute();
        }
        else
        {
            Console.WriteLine("Invalid path.");
        }
    }

    internal static void HandleFileCommand(string[] parts)
    {
        if (parts.Length < 2)
        {
            Console.WriteLine("Usage: file show [Path] {-m Mode} | file move [SourcePath] [DestinationPath] | file copy [SourcePath] [DestinationPath] | file delete [Path] | file rename [Path] [Name]");
            return;
        }

        string subCommand = parts[1].ToLower(System.Globalization.CultureInfo.CurrentCulture);

        switch (subCommand)
        {
            case "show":
                HandleFileShowCommand(parts);
                break;
            case "move":
                HandleFileMoveCommand(parts);
                break;
            case "copy":
                HandleFileCopyCommand(parts);
                break;
            case "delete":
                HandleFileDeleteCommand(parts);
                break;
            case "rename":
                HandleFileRenameCommand(parts);
                break;
            default:
                Console.WriteLine("Unknown file command.");
                break;
        }
    }

    internal static void HandleFileShowCommand(string[] parts)
    {
        if (Cwd == null)
        {
            throw new InvalidOperationException();
        }

        if (parts.Length < 3)
        {
            Console.WriteLine("Usage: file show [Path] {-m Mode}");
            return;
        }

        string path = parts[2];
        string mode = "console";

        if (parts.Length > 4 && parts[3] == "-m")
        {
            mode = parts[4];
        }

        Uri? uri = Filesystem?.GetUriByPath(path, Cwd);
        if (uri != null)
        {
            FilesystemCommand.Filesystem = Filesystem;
            var command = new FilesystemShow(uri, OutStream);
            command.Execute();
        }
        else
        {
            Console.WriteLine("Invalid path.");
        }
    }

    internal static void HandleFileMoveCommand(string[] parts)
    {
        if (Cwd == null)
        {
            throw new InvalidOperationException();
        }

        if (parts.Length < 4)
        {
            Console.WriteLine("Usage: file move [SourcePath] [DestinationPath]");
            return;
        }

        string sourcePath = parts[2];
        string destinationPath = parts[3];

        Uri? sourceUri = Filesystem?.GetUriByPath(sourcePath, Cwd);
        Uri? destinationUri = Filesystem?.GetUriByPath(destinationPath, Cwd);
        if (sourceUri != null && destinationUri != null)
        {
            FilesystemCommand.Filesystem = Filesystem;
            var command = new FilesystemMove(sourceUri, destinationUri);
            command.Execute();
        }
        else
        {
            Console.WriteLine("Invalid path.");
        }
    }

    internal static void HandleFileCopyCommand(string[] parts)
    {
        if (Cwd == null)
        {
            throw new InvalidOperationException();
        }

        if (parts.Length < 4)
        {
            Console.WriteLine("Usage: file copy [SourcePath] [DestinationPath]");
            return;
        }

        string sourcePath = parts[2];
        string destinationPath = parts[3];

        Uri? sourceUri = Filesystem?.GetUriByPath(sourcePath, Cwd);
        Uri? destinationUri = Filesystem?.GetUriByPath(destinationPath, Cwd);
        if (sourceUri != null && destinationUri != null)
        {
            FilesystemCommand.Filesystem = Filesystem;
            var command = new FilesystemCopy(sourceUri, destinationUri);
            command.Execute();
        }
        else
        {
            Console.WriteLine("Invalid path.");
        }
    }

    internal static void HandleFileDeleteCommand(string[] parts)
    {
        if (Cwd == null)
        {
            throw new InvalidOperationException();
        }

        if (parts.Length < 3)
        {
            Console.WriteLine("Usage: file delete [Path]");
            return;
        }

        string path = parts[2];

        Uri? uri = Filesystem?.GetUriByPath(path, Cwd);
        if (uri != null)
        {
            FilesystemCommand.Filesystem = Filesystem;
            var command = new FilesystemDelete(uri);
            command.Execute();
        }
        else
        {
            Console.WriteLine("Invalid path.");
        }
    }

    internal static void HandleFileRenameCommand(string[] parts)
    {
        if (Cwd == null)
        {
            throw new InvalidOperationException();
        }

        if (parts.Length < 4)
        {
            Console.WriteLine("Usage: file rename [Path] [Name]");
            return;
        }

        string path = parts[2];
        string newName = parts[3];

        Uri? uri = Filesystem?.GetUriByPath(path, Cwd);
        if (uri != null)
        {
            string noLastSegment = $"{uri.Scheme}://{uri.Authority}";

            for (int i = 0; i < uri.Segments.Length - 1; i++)
            {
                noLastSegment += uri.Segments[i];
            }

            FilesystemCommand.Filesystem = Filesystem;
            var command = new FilesystemMove(uri, new Uri(noLastSegment + newName));
            command.Execute();
        }
        else
        {
            Console.WriteLine("Invalid path.");
        }
    }
}
