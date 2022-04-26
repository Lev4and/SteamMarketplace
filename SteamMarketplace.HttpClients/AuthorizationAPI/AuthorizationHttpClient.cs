using Newtonsoft.Json;
using SteamMarketplace.Authorization.Models;
using SteamMarketplace.HttpClients.Common.Extensions;
using SteamMarketplace.Model.Common;
using System.Text;

namespace SteamMarketplace.HttpClients.AuthorizationAPI
{
    public class AuthorizationHttpClient : AuthorizationAPIHttpClient
    {
        public AuthorizationHttpClient() : base(AuthorizationAPIRoutes.AuthorizationPath)
        {

        }

        public async Task<BaseResponseModel<JWT>> LoginAsync(Login viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "The viewModel must not be empty.");
            }

            return await PostAsync<BaseResponseModel<JWT>>(AuthorizationAPIRoutes.AuthorizationQuery, viewModel);
        }
    }
}
