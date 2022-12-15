using System.Text.Json.Serialization;

namespace Folders.Core.Entities;

public class Folder : BaseEntity
{
    public string Name { get; set; }
    public int PerentId { get; set; }
    [JsonIgnore]
    public Folder? Perent { get; set; }
    [JsonIgnore]
    public ICollection<Folder>? Children { get; set; }

    public Folder()
    {
        Children = new List<Folder>();
    }
}