using SteamMarketplace.Model.Common;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class RandomizeSalesHttpClient : ResourceAPIHttpClient
    {
        public RandomizeSalesHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.RandomizeSalesPath, authorization)
        {

        }

        public async Task<BaseResponseModel<object?>> ExposeItemsOnSale()
        {
            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<object?>>(ResourceAPIRoutes.RandomizeSalesQuery);
        }
    }
}
