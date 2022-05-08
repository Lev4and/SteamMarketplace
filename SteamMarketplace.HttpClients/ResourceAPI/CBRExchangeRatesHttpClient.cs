using SteamMarketplace.HttpClients.CBR.ResponseModels;
using SteamMarketplace.Model.Common;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class CBRExchangeRatesHttpClient : ResourceAPIHttpClient
    {
        public CBRExchangeRatesHttpClient(Common.Services.Authorization authorization) : 
            base(ResourceAPIRoutes.CBRExchangeRatesPath, authorization)
        {

        }

        public async Task<BaseResponseModel<LatestExchangeRate>> GetLatestExchangeRateAsync()
        {
            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<LatestExchangeRate>>(ResourceAPIRoutes.CBRLatestExchangeRatesQuery);
        }
    }
}
