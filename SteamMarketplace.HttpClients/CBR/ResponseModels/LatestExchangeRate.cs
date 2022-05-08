using Newtonsoft.Json;
using SteamMarketplace.Model.JsonConverters;

namespace SteamMarketplace.HttpClients.CBR.ResponseModels
{
    public class LatestExchangeRate
    {
        [JsonProperty("base")]
        public string CurrencyBase { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixTimeToDatetimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
