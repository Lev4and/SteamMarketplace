using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using System.Text;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ImportItemHttpClient : ResourceAPIHttpClient
    {
        public ImportItemHttpClient(Services.Authorization authorization)
            : base(ResourceAPIRoutes.ImportItemPath, authorization)
        {

        }

        public async Task<BaseResponseModel<Guid>> ImportAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("path", "The item should not be empty.");
            }

            await AuthorizeAsync();

            return await PostAsync<BaseResponseModel<Guid>>(ResourceAPIRoutes.ImportItemQuery, item);
        }
    }
}
