using SteamMarketplace.HttpClients.CBR.ResponseModels;
using SteamMarketplace.Model.Common;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ImportExchangeRateHttpClient : ResourceAPIHttpClient
    {
        public ImportExchangeRateHttpClient(Services.Authorization authorization) : 
            base(ResourceAPIRoutes.ImportExchangeRatePath, authorization)
        {

        }

        public async Task<BaseResponseModel<bool?>> ImportAsync(DailyExchangeRate dailyExchangeRate)
        {
            if (dailyExchangeRate == null)
            {
                throw new ArgumentNullException(nameof(dailyExchangeRate));
            }

            await AuthorizeAsync();

            return await PostAsync<BaseResponseModel<bool?>>(ResourceAPIRoutes.ImportExchangeRateQuery, dailyExchangeRate);
        }
    }
}
