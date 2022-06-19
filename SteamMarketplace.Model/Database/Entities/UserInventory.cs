namespace SteamMarketplace.Model.Database.Entities
{
    public class UserInventory : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid ItemId { get; set; }

        public DateTime AddedAt { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Item Item { get; set; }
    }
}
