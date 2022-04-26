using Newtonsoft.Json;
using SteamMarketplace.Model.Json;

namespace SteamMarketplace.Model.Marketplace.CSMoney.Types
{
    public class Item : JsonObject
    {
        [JsonProperty("hasHighDemand")]
        public bool HasHighDemand { get; set; }

        [JsonProperty("hasScreenshot")]
        public bool HasScreenshot { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("appId")]
        public int AppId { get; set; }

        [JsonProperty("nameId")]
        public int NameId { get; set; }

        [JsonProperty("pattern")]
        public int? Pattern { get; set; }

        [JsonProperty("stackSize")]
        public int? StackSize { get; set; }

        [JsonProperty("assetId")]
        public long AssetId { get; set; }

        [JsonProperty("tradeLock")]
        public long? TradeLock { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("3d")]
        public string? _3d { get; set; }

        [JsonProperty("img")]
        public string? Img { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("float")]
        public string? Float { get; set; }

        [JsonProperty("rarity")]
        public string? Rarity { get; set; }

        [JsonProperty("quality")]
        public string? Quality { get; set; }

        [JsonProperty("preview")]
        public string? Preview { get; set; }

        [JsonProperty("steamId")]
        public string? SteamId { get; set; }

        [JsonProperty("stackId")]
        public string? StackId { get; set; }

        [JsonProperty("inspect")]
        public string? Inspect { get; set; }

        [JsonProperty("fullName")]
        public string? FullName { get; set; }

        [JsonProperty("steamImg")]
        public string? SteamImg { get; set; }

        [JsonProperty("screenshot")]
        public string? Screenshot { get; set; }

        [JsonProperty("collection")]
        public string? Collection { get; set; }

        [JsonProperty("id")]
        public object? Id { get; set; }

        [JsonProperty("rank")]
        public object? Rank { get; set; }

        [JsonProperty("userId")]
        public object? UserId { get; set; }

        [JsonProperty("overpay")]
        public Overpay? Overpay { get; set; }

        [JsonProperty("stickers")]
        public List<Sticker>? Stickers { get; set; }

        [JsonProperty("stackItems")]
        public List<StackItem>? StackItems { get; set; }
    }
}
