using SteamMarketplace.HttpClients.Common;

namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIHttpClient : AuthorizationHttpClient
    {
        public ResourceAPIHttpClient(string path) : base($"http://{ResourceAPIRoutes.Domain}/{path}", 
            ResourceAPIRoutes.Authorization)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path", "The path should not be empty.");
            }
        }
    }
}
