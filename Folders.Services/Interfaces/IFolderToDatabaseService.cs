using Folders.Core.Entities;

namespace Folders.Services.Interfaces;

public interface IFolderToDatabaseService
{
    public Task ImportToDatabase(Folder root);
}