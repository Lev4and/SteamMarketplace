using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Collection : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string? RuName { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as Collection;

            return Name == other?.Name && RuName == other?.RuName;
        }
    }
}
