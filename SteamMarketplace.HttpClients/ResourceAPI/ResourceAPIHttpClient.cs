using SteamMarketplace.HttpClients.Common;
using Services = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIHttpClient : AuthorizationHttpClient
    {
        public ResourceAPIHttpClient(string path, Services.Authorization authorization) : 
            base($"http://{ResourceAPIRoutes.Domain}/{path}", ResourceAPIRoutes.Authorization, authorization)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path", "The path should not be empty.");
            }
        }
    }
}
