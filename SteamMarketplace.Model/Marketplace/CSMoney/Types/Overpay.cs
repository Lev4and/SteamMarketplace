using Newtonsoft.Json;
using SteamMarketplace.Model.Json;

namespace SteamMarketplace.Model.Marketplace.CSMoney.Types
{
    public class Overpay : JsonObject
    {
        [JsonProperty("stickers")]
        public double? Stickers { get; set; }

        [JsonProperty("float")]
        public double? Float { get; set; }
    }
}
