using Itmo.ObjectOrientedProgramming.Lab4.DataUnits;

namespace Itmo.ObjectOrientedProgramming.Lab4.Filesystems;

public interface IFilesystem
{
    Uri GetUriByPath(string path, Uri localRoot);

    IDataUnit GetResourceByUri(Uri uri);

    IDataUnit Move(Uri sourceUri, Uri targetUri);

    IDataUnit Copy(Uri sourceUri, Uri targetUri);

    void Delete(Uri sourceUri);

    Stream OpenRead(Uri sourceUri);
}