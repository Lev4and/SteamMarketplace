using Newtonsoft.Json;
using SteamMarketplace.Model.Json;

namespace SteamMarketplace.Model.Marketplace.CSMoney.Types
{
    public class Sticker : JsonObject
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("wear")]
        public int Wear { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("overprice")]
        public double? Overprice { get; set; }

        [JsonProperty("img")]
        public string? Img { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("wikiLink")]
        public string? WikiLink { get; set; }
    }
}
