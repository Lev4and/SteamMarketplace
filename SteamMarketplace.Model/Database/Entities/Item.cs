using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Item : BaseEntity
    {
        public Guid ApplicationId { get; set; }

        public Guid? CollectionId { get; set; }

        public Guid? QualityId { get; set; }

        public Guid? RarityId { get; set; }

        public Guid TypeId { get; set; }

        public long AssetId { get; set; }

        public long CSMoneyId { get; set; }

        [Column(TypeName = "float(15)")]
        public float? Float { get; set; }

        public string? Name { get; set; }

        [Required]
        public string SteamId { get; set; }

        [Required]
        public string FullName { get; set; }

        public DateTime AddedAt { get; set; }

        public virtual Rarity Rarity { get; set; }

        public virtual ItemType Type { get; set; }

        public virtual Quality Quality { get; set; }

        public virtual ItemImage Image { get; set; }

        public virtual Collection Collection { get; set; }

        public virtual Application Application { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<ItemNested> Items { get; set; }

        public virtual ICollection<ItemNested> ItemNesteds { get; set; }

        public virtual ICollection<UserInventory> UserInventories { get; set; }
    }
}
