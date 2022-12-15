using Folders.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Folders.Infrastructure.Data.Configuration;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasMany(x => x.Children).WithOne(x => x.Perent);
    }
}