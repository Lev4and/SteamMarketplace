using SteamMarketplace.HttpClients.CBR.ResponseModels;

namespace SteamMarketplace.HttpClients.CBR
{
    public class DailyHttpClient : CBRHttpClient
    {
        public DailyHttpClient() : base(CBRHttpRoutes.DailyPath)
        {

        }

        public async Task<DailyExchangeRate> GetDailyExchangeRateAsync(DateTime date)
        {
            return await GetAsync<DailyExchangeRate>($"{date.ToString("yyyy")}/{date.ToString("MM")}/{date.ToString("dd")}/{CBRHttpRoutes.DailyQuery}");
        }
    }
}
