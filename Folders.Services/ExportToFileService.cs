using Folders.Services.Interfaces;
using Folders.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Folders.Core.Entities;
using Newtonsoft.Json;

namespace Folders.Services;

public class ExportToFileService : IExportToFileService
{
    private readonly FoldersDbContext _foldersDbContext;

    public ExportToFileService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async Task ExportToFile()
    {
        var folders = await _foldersDbContext.Folders.ToListAsync();

        using (FileStream fs = new FileStream("folders.json", FileMode.Create))
        {
            var sw = new StreamWriter(fs);
            var json = JsonConvert.SerializeObject(folders);
            await sw.WriteLineAsync(json);
            sw.Close();
        }
    }
}