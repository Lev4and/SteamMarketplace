using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.HttpClients.CSMoney.ResponseModels;

namespace SteamMarketplace.HttpClients.CSMoney
{
    public class StoreHttpClient : CSMoneyHttpClient
    {
        public StoreHttpClient() : base(CSMoneyRoutes.StoreDomain, CSMoneyRoutes.StorePath)
        {

        }

        public async Task<BotInventory> GetInventoryAsync(int limit, int offset, decimal minPrice = 0, decimal maxPrice = 30000, bool withStack = true)
        {
            if (limit <= 0 || limit > 60)
            {
                throw new ArgumentOutOfRangeException("limit", "The limit must not be less than or equal " +
                    "to zero and must not be more than sixty.");
            }

            if (offset < 0 || offset > 5000)
            {
                throw new ArgumentOutOfRangeException("offset", "The offset should not be less than zero and " +
                    "should not be more than five thousand.");
            }

            UseHeaders(CSMoneyHeaders.JsonHeaders);
            UseCookie();

            return await GetAsync<BotInventory>($"730?limit={limit}&offset={offset}" +
                $"&minPrice={minPrice}&maxPrice={maxPrice}&withStack={withStack.ToString().ToLower()}");
        }
    }
}
