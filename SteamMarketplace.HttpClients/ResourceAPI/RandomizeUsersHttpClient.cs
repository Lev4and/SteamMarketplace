using SteamMarketplace.Model.Common;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class RandomizeUsersHttpClient : ResourceAPIHttpClient
    {
        public RandomizeUsersHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.RandomizeUsersPath, authorization)
        {

        }

        public async Task<BaseResponseModel<object?>> Create()
        {
            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<object?>>(ResourceAPIRoutes.RandomizeUsersQuery);
        }
    }
}
