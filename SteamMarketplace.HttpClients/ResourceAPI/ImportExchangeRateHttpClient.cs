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

        public async Task<BaseResponseModel<bool?>> ImportAsync(LatestExchangeRate latestExchangeRate)
        {
            if (latestExchangeRate == null)
            {
                throw new ArgumentNullException(nameof(latestExchangeRate));
            }

            await AuthorizeAsync();

            return await PostAsync<BaseResponseModel<bool?>>(ResourceAPIRoutes.ImportExchangeRateQuery, latestExchangeRate);
        }
    }
}
