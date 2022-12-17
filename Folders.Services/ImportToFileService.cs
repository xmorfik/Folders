using Folders.Core.Entities;
using Folders.Infrastructure.Data;
using Folders.Services.Interfaces;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text.Json;

namespace Folders.Services;

public class ImportToFileService : IImportToFileService
{
    private readonly FoldersDbContext _foldersDbContext;

    public ImportToFileService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async Task ImportToFile()
    {
        var folders = new List<Folder>();
        using (FileStream fs = new FileStream("folders.json", FileMode.Open))
        {
            var sr = new StreamReader(fs);
            var json = await sr.ReadToEndAsync();
            folders = JsonConvert.DeserializeObject<List<Folder>>(json);
            sr.Close();
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