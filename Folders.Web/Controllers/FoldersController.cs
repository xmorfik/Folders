using Folders.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Folders.Web.Controllers;

public class FoldersController : Controller
{
    private readonly IFolderService _folderService;
    private readonly IExportFoldersService _exportFoloderService;

    public FoldersController(IFolderService folderService,
        IExportFoldersService exportFoldersService)
    {
        _folderService = folderService;
        _exportFoloderService = exportFoldersService;
    }

    public async ValueTask<IActionResult> Index(int id)
    {
        var root = await _folderService.Get(id);
        return View(root);
    }


    public async Task<IActionResult> Export()
    {
        await _exportFoloderService.Export();
        return RedirectToAction("Index", "Folders");
    }

    public IActionResult Import()
    { 
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Import(IFormFile file)
    {

        return Ok();
    }
}
