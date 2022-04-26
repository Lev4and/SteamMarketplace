using SteamMarketplace.HttpClients.Common;

namespace SteamMarketplace.HttpClients.AuthorizationAPI
{
    public class AuthorizationAPIHttpClient : BaseHttpClient
    {
        public AuthorizationAPIHttpClient(string path) : base($"http://{AuthorizationAPIRoutes.Domain}/{path}")
        {
            if (path == null)
            {
                throw new ArgumentNullException("path", "The path should not be empty.");
            }
        }
    }
}
