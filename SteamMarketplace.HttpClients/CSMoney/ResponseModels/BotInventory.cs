using Newtonsoft.Json;
using SteamMarketplace.Model.Json;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;

namespace SteamMarketplace.HttpClients.CSMoney.ResponseModels
{
    public class BotInventory : JsonObject
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
