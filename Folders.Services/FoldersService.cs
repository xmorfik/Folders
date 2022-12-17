using Folders.Core.Entities;
using Folders.Infrastructure.Data;
using Folders.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Folders.Services;

public class FoldersService : IFoldersService
{
    private readonly FoldersDbContext _foldersDbContext;

    public FoldersService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async Task<Folder> Get(int id)
    {
        try
        {
            if (id == 0)
            {
                var folder = await _foldersDbContext.Folders.FirstOrDefaultAsync();
                if (folder == null)
                {
                    return new Folder();
                }
                var children = await _foldersDbContext.Folders.Where(x => x.PerentId == folder.Id).ToListAsync();
                folder.Children = children;
                return folder;
            }
            else
            {
                var folder = await _foldersDbContext.Folders.FindAsync(id);
                var children = await _foldersDbContext.Folders.Where(x => x.PerentId == id).ToListAsync();
                folder.Children = children;
                return folder;
            }
        }
        catch
        {
            throw new Exception();
        }
    }
}