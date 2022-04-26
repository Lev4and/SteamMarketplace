using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.Model.Common;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using System.Text;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ImportItemHttpClient : ResourceAPIHttpClient
    {
        public ImportItemHttpClient() : base(ResourceAPIRoutes.ImportItemPath)
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
