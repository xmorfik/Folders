using Folders.Core.Entities;
using Folders.Infrastructure.Data;
using Folders.Services.Interfaces;

namespace Folders.Services;

public class FoldersToDatabaseService : IFoldersToDatabaseService
{
    private readonly FoldersDbContext _foldersDbContext;

    public FoldersToDatabaseService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async Task ImportToDatabase(Folder root)
    {
        _foldersDbContext.Database.EnsureDeleted();
        _foldersDbContext.Database.EnsureCreated();
        _foldersDbContext.Folders.Add(root);
        await _foldersDbContext.SaveChangesAsync();
    }
}