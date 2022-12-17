using Folders.Core.Entities;

namespace Folders.Services.Interfaces;

public interface IFolderService
{
    public Task<Folder> Get(int id);
}
