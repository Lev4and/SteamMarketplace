using Newtonsoft.Json;

namespace SteamMarketplace.HttpClients.CBR.ResponseModels
{
    public class ExchangeRate
    {
        [JsonProperty("Nominal")]
        public int Nominal { get; set; }

        [JsonProperty("Value")]
        public decimal Value { get; set; }

        [JsonProperty("Previous")]
        public decimal Previous { get; set; }

        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("NumCode")]
        public string NumCode { get; set; }

        [JsonProperty("CharCode")]
        public string CharCode { get; set; }
    }
}
