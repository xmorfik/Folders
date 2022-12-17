using Folders.Core.Entities;
using Folders.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Folders.Web.Controllers;

public class FoldersController : Controller
{
    private readonly IFoldersService _foldersService;
    private readonly IExportToFileService _exportToFileService;
    private readonly IImportToFileService _importToFileService;
    private readonly IFileSystemSnapshotService _fileSystemSnapshotService;
    private readonly IFoldersToDatabaseService _folderToDatabaseService;

    public FoldersController(IFoldersService foldersService,
        IExportToFileService exportToFileService,
        IFileSystemSnapshotService fileSystemSnapshotService,
        IFoldersToDatabaseService folderToDatabaseService,
        IImportToFileService importToFileService)
    {
        _foldersService = foldersService;
        _exportToFileService = exportToFileService;
        _fileSystemSnapshotService = fileSystemSnapshotService;
        _folderToDatabaseService = folderToDatabaseService;
        _importToFileService = importToFileService;
    }

    public async Task<IActionResult> Index(int id)
    {
        var root = new Folder();

        try
        {
            root = await _foldersService.Get(id);
        }
        catch
        {
        }

        return View(root);
    }


    public async Task<IActionResult> ExportToFile()
    {
        try
        {
            await _exportToFileService.ExportToFile();
        }
        catch
        {
        }

        return RedirectToAction("Index", "Folders");
    }

    public async Task<IActionResult> ImportFromFile()
    {
        try
        {
            await _importToFileService.ImportToFile();
        }
        catch
        {
        }
        
        return RedirectToAction("Index", "Folders");
    }

    [HttpGet]
    public IActionResult ImportFromFileSystem()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImportFromFileSystem(string path)
    {
        try
        {
            await _exportToFileService.ExportToFile();
            var folder = _fileSystemSnapshotService.GetSnapshot(path);
            await _folderToDatabaseService.ImportToDatabase(folder);
        }
        catch
        {
        }

        return RedirectToAction("Index", "Folders");
    }
}
