namespace SteamMarketplace.Model.Database.AnonymousTypes
{
    public class RandomUserInventoryItem
    {
        public Guid ItemId { get; set; }

        public string ItemFullName { get; set; }

        public string ItemSteamImage { get; set; }

        public decimal PriceUsd { get; set; }
    }
}
