﻿namespace SteamMarketplace.Model.Database.Entities
{
    public class ItemType : BaseEntity
    {
        public int CSMoneyId { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as ItemType;

            return CSMoneyId == other?.CSMoneyId;
        }
    }
}
