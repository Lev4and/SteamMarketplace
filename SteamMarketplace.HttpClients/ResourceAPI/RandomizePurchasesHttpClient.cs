using SteamMarketplace.Model.Common;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class RandomizePurchasesHttpClient : ResourceAPIHttpClient
    {
        public RandomizePurchasesHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.RandomizePurchasesPath, authorization)
        {

        }

        public async Task<BaseResponseModel<object?>> BuyItems()
        {
            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<object?>>(ResourceAPIRoutes.RandomizePurchasesQuery);
        }
    }
}
