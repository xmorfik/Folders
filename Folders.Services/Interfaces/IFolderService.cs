using Folders.Core.Entities;

namespace Folders.Services.Interfaces;

public interface IFolderService
{
    public ValueTask<Folder> Get(int id);
}
