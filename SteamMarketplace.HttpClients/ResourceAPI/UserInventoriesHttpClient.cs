using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class UserInventoriesHttpClient : ResourceAPIHttpClient
    {
        public UserInventoriesHttpClient() : base(ResourceAPIRoutes.UserInventoriesPath)
        {

        }

        public async Task<PagedResponseModel<UserInventory>> GetUserInventoryAsync(UserInventoriesFilters filters)
        {
            await AuthorizeAsync();

            return await PostAsync<PagedResponseModel<UserInventory>>(ResourceAPIRoutes.UserInventoriesQuery, filters);
        }
    }
}
