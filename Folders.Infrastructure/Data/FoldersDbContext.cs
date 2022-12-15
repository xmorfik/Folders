using Folders.Core.Entities;
using Folders.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Folders.Infrastructure.Data;

public class FoldersDbContext : DbContext
{
    public DbSet<Folder> Folders { get; set; }

    public FoldersDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new FolderConfiguration());
        base.OnModelCreating(builder);
    }
}