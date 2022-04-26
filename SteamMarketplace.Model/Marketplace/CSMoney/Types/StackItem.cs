using Newtonsoft.Json;
using SteamMarketplace.Model.Json;

namespace SteamMarketplace.Model.Marketplace.CSMoney.Types
{
    public class StackItem : JsonObject
    {
        [JsonProperty("hasScreenshot")]
        public bool HasScreenshot { get; set; }

        [JsonProperty("pattern")]
        public int? Pattern { get; set; }

        [JsonProperty("3d")]
        public string? _3d { get; set; }

        [JsonProperty("img")]
        public string? Img { get; set; }

        [JsonProperty("float")]
        public string? Float { get; set; }

        [JsonProperty("steamId")]
        public string? SteamId { get; set; }

        [JsonProperty("stackId")]
        public string? StackId { get; set; }

        [JsonProperty("preview")]
        public string? Preview { get; set; }

        [JsonProperty("screenshot")]
        public string? Screenshot { get; set; }

        [JsonProperty("id")]
        public object? Id { get; set; }

        [JsonProperty("tradeLock")]
        public object? TradeLock { get; set; }
    }
}
