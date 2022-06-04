using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.AnonymousTypes
{
    public class ExtendedItem
    {
        public int Count { get; set; }

        public int CountOwners { get; set; }

        public double Rarity { get; set; }

        public float? AverageFloat { get; set; }

        public DateTime AddedAt { get; set; }

        public Item Item { get; set; }
    }
}
