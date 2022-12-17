using Folders.Services.Interfaces;
using Folders.Services;

namespace Folders.Web.Extentions;

public static class ConfigureServicesExtention
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFoldersService, FoldersService>();
        services.AddScoped<IExportToFileService, ExportToFileService>();
        services.AddScoped<IImportToFileService, ImportToFileService>();
        services.AddScoped<IFileSystemSnapshotService, FileSystemSnapshotService>();
        services.AddScoped<IFoldersToDatabaseService, FoldersToDatabaseService>();

        return services;
    }
}
