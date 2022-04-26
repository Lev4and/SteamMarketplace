using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Rarity
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? RuName { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
