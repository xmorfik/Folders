using Folders.Core.Entities;
using Folders.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Folders.Web.Controllers;

public class FoldersController : Controller
{
    private readonly IFolderService _folderService;
    private readonly IExportToFileService _exportToFileService;
    private readonly IImportToFileService _importToFileService;
    private readonly IFileSystemSnapshotService _fileSystemSnapshotService;
    private readonly IFolderToDatabaseService _folderToDatabaseService;

    public FoldersController(IFolderService folderService,
        IExportToFileService exportFoldersService,
        IFileSystemSnapshotService fileSystemSnapshotService,
        IFolderToDatabaseService folderToDatabaseService,
        IImportToFileService importFoloderService)
    {
        _folderService = folderService;
        _exportToFileService = exportFoldersService;
        _fileSystemSnapshotService = fileSystemSnapshotService;
        _folderToDatabaseService = folderToDatabaseService;
        _importToFileService = importFoloderService;
    }

    public async Task<IActionResult> Index(int id)
    {
        var root = new Folder();

        try
        {
            root = await _folderService.Get(id);
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

            await _importToFileService.ImportToFile();
    
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
