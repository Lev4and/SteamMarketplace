namespace SteamMarketplace.Model.Database.AnonymousTypes
{
    public class GroupedItem
    {
        public int Count { get; set; }

        public decimal MinPriceUsd { get; set; }

        public string FullName { get; set; }

        public string SteamImg { get; set; }
    }
}
