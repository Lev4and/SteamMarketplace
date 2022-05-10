using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class SalesHttpClient : ResourceAPIHttpClient
    {
        public SalesHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.SalesPath, authorization)
        {

        }

        public async Task<PagedResponseModel<Sale>> GetMySalesAsync(SalesFilters filters)
        {
            await AuthorizeAsync();

            return await PostAsync<PagedResponseModel<Sale>>(ResourceAPIRoutes.SalesMySalesQuery, filters);
        }
    }
}
