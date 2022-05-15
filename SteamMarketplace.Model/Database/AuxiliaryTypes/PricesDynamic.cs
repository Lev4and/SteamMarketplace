namespace SteamMarketplace.Model.Database.AuxiliaryTypes
{
    public class PricesDynamic
    {
        public int CountSold { get; set; }

        public decimal MinPriceUsd { get; set; }

        public DateTime Date { get; set; }
    }
}
