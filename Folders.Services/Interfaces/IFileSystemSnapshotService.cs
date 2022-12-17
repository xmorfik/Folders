using Folders.Core.Entities;

namespace Folders.Services.Interfaces;

public interface IFileSystemSnapshotService
{
    public Folder GetSnapshot(string path);
}
