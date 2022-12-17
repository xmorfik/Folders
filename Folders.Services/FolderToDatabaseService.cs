using Folders.Core.Entities;
using Folders.Infrastructure.Data;
using Folders.Services.Interfaces;

namespace Folders.Services;

public class FolderToDatabaseService : IFolderToDatabaseService
{
    private readonly FoldersDbContext _foldersDbContext;

    public FolderToDatabaseService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async Task Import(Folder root)
    {
        _foldersDbContext.Database.EnsureDeleted();
        _foldersDbContext.Database.EnsureCreated();
        _foldersDbContext.Folders.Add(root);
        await _foldersDbContext.SaveChangesAsync();
    }
}