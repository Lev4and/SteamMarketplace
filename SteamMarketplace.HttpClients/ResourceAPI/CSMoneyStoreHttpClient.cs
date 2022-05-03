using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.HttpClients.CSMoney.ResponseModels;
using SteamMarketplace.Model.Common;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class CSMoneyStoreHttpClient : ResourceAPIHttpClient
    {
        public CSMoneyStoreHttpClient() : base(ResourceAPIRoutes.CSMoneyStorePath)
        {

        }

        public async Task<BaseResponseModel<BotInventory>> GetInventoryAsync(int limit, int offset, bool withStack = true)
        {
            if (limit <= 0)
            {
                throw new ArgumentOutOfRangeException("limit", "The limit must not be less than or equal to zero.");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "The offset must not be less than zero.");
            }

            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<BotInventory>>($"{ResourceAPIRoutes.CSMoneyStoreQuery}?limit={limit}" +
                $"&offset={offset}&withStack={withStack.ToString().ToLower()}");
        }
    }
}
