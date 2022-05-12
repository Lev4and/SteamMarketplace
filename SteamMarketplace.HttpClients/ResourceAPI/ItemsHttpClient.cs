using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ItemsHttpClient : ResourceAPIHttpClient
    {
        public ItemsHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.ItemsPath, authorization)
        {

        }

        public async Task<BaseResponseModel<List<string>>> GetSearchSuggestionsAsync(string searchString)
        {
            await AuthorizeAsync();

            return await GetAsync<BaseResponseModel<List<string>>>($"{ResourceAPIRoutes.ItemsSearchSuggestionsQuery}" +
                $"?q={searchString}");
        }

        public async Task<PagedResponseModel<GroupedItem>> GetGroupedItemsAsync(ItemsFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            await AuthorizeAsync();

            return await PostAsync<PagedResponseModel<GroupedItem>>(ResourceAPIRoutes.ItemsGroupedItemsQuery, filters);
        }
    }
}
