using System.ComponentModel.DataAnnotations.Schema;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Sale : BaseEntity
    {
        public Guid SellerId { get; set; }

        public Guid ItemId { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal PriceUsd { get; set; }

        public DateTime ExposedAt { get; set; }

        public DateTime? SoldAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        public virtual ApplicationUser Seller { get; set; }

        public virtual Purchase Purchase { get; set; }

        public virtual Item Item { get; set; }
    }
}
