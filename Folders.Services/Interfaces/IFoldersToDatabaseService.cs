using Folders.Core.Entities;

namespace Folders.Services.Interfaces;

public interface IFoldersToDatabaseService
{
    public Task ImportToDatabase(Folder root);
}