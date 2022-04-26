using Microsoft.AspNetCore.Identity;

namespace SteamMarketplace.Model.Database.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<UserInventory> Inventory { get; set; }
    }
}
