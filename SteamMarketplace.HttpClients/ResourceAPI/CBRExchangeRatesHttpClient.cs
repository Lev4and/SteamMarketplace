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

        public async Task<BaseResponseModel<DailyExchangeRate>> GetLatestExchangeRateAsync()
        {
            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<DailyExchangeRate>>(ResourceAPIRoutes.CBRLatestExchangeRatesQuery);
        }
    }
}
