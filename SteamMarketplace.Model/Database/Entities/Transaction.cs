using System.ComponentModel.DataAnnotations.Schema;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TypeId { get; set; }

        public Guid? PurchaseId { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Value { get; set; }

        public DateTime HappenedAt { get; set; }

        public virtual TransactionType Type { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}
