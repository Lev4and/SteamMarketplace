using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class PurchasesHttpClient : ResourceAPIHttpClient
    {
        public PurchasesHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.PurchasesPath, authorization)
        {

        }

        public async Task<PagedResponseModel<Purchase>> GetMyPurchasesAsync(PurchasesFilters filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            await AuthorizeAsync();

            return await PostAsync<PagedResponseModel<Purchase>>(ResourceAPIRoutes.PurchasesMyPurchasesQuery, filters);
        }
    }
}
