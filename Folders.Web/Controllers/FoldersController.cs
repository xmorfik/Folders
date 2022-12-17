using Folders.Core.Entities;
using Folders.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Folders.Web.Controllers;

public class FoldersController : Controller
{
    private readonly IFolderService _folderService;
    private readonly IExportFoldersService _exportFoloderService;
    private readonly IImportFoldersService _importFoloderService;
    private readonly IFileSystemSnapshotService _fileSystemSnapshotService;
    private readonly IFolderToDatabaseService _folderToDatabaseService;

    public FoldersController(IFolderService folderService,
        IExportFoldersService exportFoldersService,
        IFileSystemSnapshotService fileSystemSnapshotService,
        IFolderToDatabaseService folderToDatabaseService,
        IImportFoldersService importFoloderService)
    {
        _folderService = folderService;
        _exportFoloderService = exportFoldersService;
        _fileSystemSnapshotService = fileSystemSnapshotService;
        _folderToDatabaseService = folderToDatabaseService;
        _importFoloderService = importFoloderService;
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
            await _exportFoloderService.Export();
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
            await _importFoloderService.Import();
        }
        catch
        {
        }

        return RedirectToAction("Index", "Folders");
    }

    [HttpGet]
    public async Task<IActionResult> ImportFromFileSystem()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImportFromFileSystem(string path)
    {
        try
        {
            await _exportFoloderService.Export();
            var folder = _fileSystemSnapshotService.GetSnapshot(path);
            await _folderToDatabaseService.Import(folder);
        }
        catch
        {
        }

        return RedirectToAction("Index", "Folders");
    }
}
