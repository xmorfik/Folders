using Folders.Services.Interfaces;
using System.Text.Json;
using Folders.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Folders.Core.Entities;

namespace Folders.Services;

public class ExportFoldersService : IExportFoldersService
{
    private readonly FoldersDbContext _foldersDbContext;

    public ExportFoldersService(FoldersDbContext foldersDbContext)
    {
        _foldersDbContext = foldersDbContext;
    }

    public async Task Export()
    {
        var folders = await _foldersDbContext.Folders.ToListAsync();

        using (FileStream fs = new FileStream("folders.json", FileMode.OpenOrCreate))
        {
            await JsonSerializer.SerializeAsync<ICollection<Folder>>(fs, folders);
        }
    }
}