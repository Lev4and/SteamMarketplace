using SteamMarketplace.HttpClients.CBR.ResponseModels;

namespace SteamMarketplace.HttpClients.CBR
{
    public class LatestHttpClient : CBRHttpClient
    {
        public LatestHttpClient() : base(CBRHttpRoutes.LatestPath)
        {

        }

        public async Task<LatestExchangeRate> GetLatestExchangeRateAsync()
        {
            return await GetAsync<LatestExchangeRate>(CBRHttpRoutes.LatestQuery);
        }
    }
}
