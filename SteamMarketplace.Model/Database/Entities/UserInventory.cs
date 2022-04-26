﻿namespace SteamMarketplace.Model.Database.Entities
{
    public class UserInventory
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ItemId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Item Item { get; set; }
    }
}
