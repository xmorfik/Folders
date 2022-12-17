using Folders.Core.Entities;

namespace Folders.Services.Interfaces;

public interface IFoldersService
{
    public Task<Folder> Get(int id);
}
