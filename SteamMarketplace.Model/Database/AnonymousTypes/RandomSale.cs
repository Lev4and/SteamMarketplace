using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.AnonymousTypes
{
    public class RandomSale : Sale
    {
        public string SellerName { get; set; }

        public string ItemFullName { get; set; }

        public string ItemSteamImage { get; set; }
    }
}
