using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;
using Itmo.ObjectOrientedProgramming.Lab4.Filesystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Directories.DirectoryIterator;

public class BfsDirectoryIterator(IFilesystem filesystem, int maxDepth)
{
    protected int MaxDepth { get; init; } = maxDepth;

    protected IFilesystem Filesystem { get; init; } = filesystem;

    public IEnumerable<IDataUnit> GetContents(IDataUnit root)
    {
        var queue = new Queue<(IDataUnit, int)>();
        queue.Enqueue((root, 0));

        var visitor = new DataUnitVisitor();

        while (queue.Count > 0)
        {
            (IDataUnit next, int depth) = queue.Dequeue();

            if (depth > MaxDepth)
            {
                continue;
            }

            next.Accept(visitor);

            if (visitor.Directory != null)
            {
                var entities = new List<IDataUnit>();

                entities.AddRange(visitor.Directory.SubDirectories);
                entities.AddRange(visitor.Directory.Files);

                foreach (IDataUnit item in entities)
                {
                    queue.Enqueue((item, depth + 1));
                }
            }

            yield return next;
        }

        yield break;
    }
}