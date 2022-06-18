using System.ComponentModel.DataAnnotations.Schema;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Purchase : BaseEntity
    {
        public Guid BuyerId { get; set; }

        public Guid SaleId { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal PriceUsd { get; set; }

        public DateTime PurchaseAt { get; set; }

        public virtual ApplicationUser Buyer { get; set; }

        public virtual Sale Sale { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
