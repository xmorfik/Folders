using Folders.Core.Entities;
using Folders.Services.Interfaces;

namespace Folders.Services;

public class FileSystemSnapshotService : IFileSystemSnapshotService
{
    private readonly EnumerationOptions _options;

    public FileSystemSnapshotService()
    {
        _options = new EnumerationOptions()
        {
            IgnoreInaccessible = true
        };
    }

    public Folder GetSnapshot(string path)
    {
        var dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            throw new FileNotFoundException();
        }
        var folder = new Folder(dir);
        Traverse(dir, folder);
        return folder;
    }

    private void Traverse(DirectoryInfo directory, Folder root)
    {
        foreach (var subDir in directory.EnumerateDirectories("*", _options))
        {
            var subFolder = new Folder(subDir);
            subFolder.Perent = root;
            root.Children.Add(subFolder);
            Traverse(subDir, subFolder);
        }
    }
}
