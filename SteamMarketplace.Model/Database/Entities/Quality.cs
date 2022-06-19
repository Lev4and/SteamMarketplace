using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Quality : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string? RuName { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as Quality;

            return Name == other?.Name && RuName == other?.RuName;
        }
    }
}
