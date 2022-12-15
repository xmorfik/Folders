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
        //long size = files.Sum(f => f.Length);

        //var filePaths = new List<string>();
        //foreach (var formFile in files)
        //{
        //    if (formFile.Length > 0)
        //    {
        //        // full path to file in temp location
        //        var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
        //    }
        //}

        return Ok();
    }
}
