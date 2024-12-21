using Itmo.ObjectOrientedProgramming.Lab4.DataUnits;
using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.DataUnitsVisitors;
using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Directories;
using Itmo.ObjectOrientedProgramming.Lab4.DataUnits.Files;

namespace Itmo.ObjectOrientedProgramming.Lab4.Filesystems;

public class LocalFilesystem : IFilesystem
{
    public Uri GetUriByPath(string path, Uri localRoot)
    {
        string root = localRoot.AbsoluteUri;

        if (!root.EndsWith('\\') && !root.EndsWith('/'))
        {
            root = $"{root}\\";
        }

        return new Uri(new Uri(root), new Uri(path));
    }

    public IDataUnit GetResourceByUri(Uri uri)
    {
        if (System.IO.File.Exists(uri.LocalPath))
        {
            return new LocalFile(uri.LocalPath);
        }

        if (System.IO.Directory.Exists(uri.LocalPath))
        {
            return new LocalDirectory(uri.LocalPath);
        }

        throw new FileNotFoundException();
    }

    public IDataUnit Move(Uri sourceUri, Uri targetUri)
    {
        IDataUnit source = GetResourceByUri(sourceUri);

        var source_visitor = new DataUnitVisitor();
        source.Accept(source_visitor);

        if (source_visitor.File != null && System.IO.Directory.Exists(targetUri.LocalPath))
        {
            System.IO.File.Move(sourceUri.LocalPath, GetUriByPath(Path.GetFileName(sourceUri.LocalPath), targetUri).LocalPath);
        }
        else if (source_visitor.File != null)
        {
            System.IO.File.Move(sourceUri.LocalPath, targetUri.LocalPath);
        }
        else
        {
            throw new InvalidOperationException("mismatched resource types");
        }

        return GetResourceByUri(targetUri);
    }

    public IDataUnit Copy(Uri sourceUri, Uri targetUri)
    {
        IDataUnit source = GetResourceByUri(sourceUri);

        var source_visitor = new DataUnitVisitor();
        source.Accept(source_visitor);

        if (source_visitor.File != null && System.IO.Directory.Exists(targetUri.LocalPath))
        {
            System.IO.File.Copy(sourceUri.LocalPath, GetUriByPath(Path.GetFileName(sourceUri.LocalPath), targetUri).LocalPath);
        }
        else if (source_visitor.File != null)
        {
            System.IO.File.Copy(sourceUri.LocalPath, targetUri.LocalPath);
        }
        else
        {
            throw new InvalidOperationException("mismatched resource types");
        }

        return GetResourceByUri(targetUri);
    }

    public void Delete(Uri sourceUri)
    {
        IDataUnit source = GetResourceByUri(sourceUri);

        var source_visitor = new DataUnitVisitor();
        source.Accept(source_visitor);

        if (source_visitor.File != null)
        {
            System.IO.File.Delete(sourceUri.LocalPath);
        }
        else if (source_visitor.Directory != null)
        {
            System.IO.Directory.Delete(sourceUri.LocalPath);
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    public Stream OpenRead(Uri sourceUri)
    {
        return new LocalFile(sourceUri.LocalPath).FileStream;
    }
}