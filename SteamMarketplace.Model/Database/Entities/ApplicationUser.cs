using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamMarketplace.Model.Database.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid CurrencyId { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal WalletBalance { get; set; }

        public DateTime RegisteredAt { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<UserInventory> Inventory { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
