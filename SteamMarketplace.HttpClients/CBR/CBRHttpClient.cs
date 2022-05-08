using SteamMarketplace.HttpClients.Common;

namespace SteamMarketplace.HttpClients.CBR
{
    public class CBRHttpClient : BaseHttpClient
    {
        public CBRHttpClient(string path) : base($"https://{CBRHttpRoutes.Domain}/{path}")
        {
            if (path == null)
            {
                throw new ArgumentNullException("path", "The path should not be empty.");
            }
        }
    }
}
