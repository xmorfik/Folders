using Folders.Services.Interfaces;
using Folders.Services;

namespace Folders.Web.Extentions;

public static class ConfigureServicesExtention
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFolderService, FolderService>();
        services.AddScoped<IExportFoldersService, ExportFoldersService>();
        services.AddScoped<IImportFoldersService, ImportFolderService>();
        services.AddScoped<IFileSystemSnapshotService, FileSystemSnapshotService>();
        services.AddScoped<IFolderToDatabaseService, FolderToDatabaseService>();

        return services;
    }
}
