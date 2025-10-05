using Itmo.ObjectOrientedProgramming.Lab4.DataUnits;
using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.FilesystemCommands;

public class FilesystemList(Uri root, int maxDepth) : FilesystemCommand
{
    private bool _executed = false;

    public override void Execute()
    {
        if (!_executed && Filesystem != null)
        {
            _executed = true;

            IDataUnit dir = Filesystem.GetResourceByUri(root);

            var visitor = new DataUnitVisitor();

            dir.Accept(visitor);

            if (visitor.Directory != null)
            {
                var contents = new List<IDataUnit>();

                contents.AddRange(visitor.Directory.SubDirectories);
                contents.AddRange(visitor.Directory.Files);

                contents.Sort();

                WriteTree(contents, string.Empty, 0);
            }
        }
    }

    private void WriteTree(IEnumerable<IDataUnit> resources, string prefix, int depth)
    {
        if (depth > maxDepth)
        {
            return;
        }

        var visitor = new DataUnitVisitor();

        foreach (IDataUnit resource in resources)
        {
            Write($"{prefix}{(depth == 0 ? "└─" : "├─")}{Path.GetFileName(resource.Uri.LocalPath)}\n");

            resource.Accept(visitor);

            if (visitor.Directory != null)
            {
                var children = new List<IDataUnit>();

                children.AddRange(visitor.Directory.SubDirectories);
                children.AddRange(visitor.Directory.Files);

                children.Sort();

                WriteTree(children, prefix + (depth == 0 ? "  " : "│ "), depth + 1);
            }
        }
    }

    private void Write(string text)
    {
        Console.Write(text);
    }
}