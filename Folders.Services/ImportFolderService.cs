using Folders.Core.Entities;
using Folders.Infrastructure.Data;
using Folders.Services.Interfaces;
using System.Text.Json;

namespace Folders.Services;

public class ImportFolderService : IImportFoldersService
{
    private readonly FoldersDbContext _foldersDbContext;

    public ImportFolderService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async Task Import()
    {
        var folders = new List<Folder>();
        using (FileStream fs = new FileStream("folders.json", FileMode.Open))
        {
            folders = await JsonSerializer.DeserializeAsync<List<Folder>>(fs);
        }
        await AddToDatabase(folders);
    }

    private async Task AddToDatabase(ICollection<Folder> folders)
    {
        _foldersDbContext.Database.EnsureDeleted();
        _foldersDbContext.Database.EnsureCreated();
        foreach (var folder in folders)
        {
            _foldersDbContext.Folders.Add(folder);
        }
        await _foldersDbContext.SaveChangesAsync();
    }
}