using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class UserInventoriesHttpClient : ResourceAPIHttpClient
    {
        public UserInventoriesHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.UserInventoriesPath, authorization)
        {

        }

        public async Task<PagedResponseModel<UserInventory>> GetUserInventoryAsync(UserInventoriesFilters filters)
        {
            await AuthorizeAsync();

            return await PostAsync<PagedResponseModel<UserInventory>>(ResourceAPIRoutes.UserInventoriesQuery, filters);
        }
    }
}
