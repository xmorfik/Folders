using Folders.Core.Entities;
using Folders.Infrastructure.Data;
using Folders.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Folders.Services;

public class FolderService : IFolderService
{
    private readonly FoldersDbContext _foldersDbContext;

    public FolderService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async ValueTask<Folder> Get(int id)
    {
        if (id == 0)
        {
            return await GetRoot();
        }

        try
        {
            var folder = await _foldersDbContext.Folders.FindAsync(id);
            var children = await _foldersDbContext.Folders.Where(x => x.PerentId == id).ToListAsync();
            folder.Children = children;
            return folder;
        }
        catch
        {
            throw new Exception();
        }
    }

    private async ValueTask<Folder> GetRoot()
    {
        try
        {
            var folder = await _foldersDbContext.Folders.FirstOrDefaultAsync();
            var children = await _foldersDbContext.Folders.Where(x => x.PerentId == folder.Id)
                                                          .Where(x => x.Id != folder.Id).ToListAsync();
            folder.Children = children;
            return folder;
        }
        catch
        {
            throw new Exception();
        }
    }
}