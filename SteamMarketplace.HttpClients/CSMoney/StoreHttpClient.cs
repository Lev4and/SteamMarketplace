using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.HttpClients.CSMoney.ResponseModels;

namespace SteamMarketplace.HttpClients.CSMoney
{
    public class StoreHttpClient : CSMoneyHttpClient
    {
        public StoreHttpClient() : base(CSMoneyRoutes.StoreDomain, CSMoneyRoutes.StorePath)
        {

        }

        public async Task<BotInventory> GetInventoryAsync(int limit, int offset, bool withStack = true)
        {
            if (limit <= 0)
            {
                throw new ArgumentOutOfRangeException("limit", "The limit must not be less than or equal to zero.");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "The offset must not be less than zero.");
            }

            UseHeaders(CSMoneyHeaders.JsonHeaders);
            UseCookie();

            return await GetAsync<BotInventory>($"730?limit={limit}&offset={offset}" +
                $"&withStack={withStack.ToString().ToLower()}");
        }
    }
}
