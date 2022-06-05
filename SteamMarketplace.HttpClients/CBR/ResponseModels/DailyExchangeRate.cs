using Newtonsoft.Json;

namespace SteamMarketplace.HttpClients.CBR.ResponseModels
{
    public class DailyExchangeRate
    {
        [JsonProperty("PreviousURL")]
        public string PreviousURL { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("PreviousDate")]
        public DateTime PreviousDate { get; set; }

        [JsonProperty("Valute")]
        public Dictionary<string, ExchangeRate> Valute { get; set; }
    }
}
