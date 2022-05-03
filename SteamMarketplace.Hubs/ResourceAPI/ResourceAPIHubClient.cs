namespace SteamMarketplace.Hubs.ResourceAPI
{
    public class ResourceAPIHubClient : Common.BaseHubClient
    {
        public ResourceAPIHubClient(string path) : base($"http://{ResourceAPIRoutes.Domain}/{path}")
        {
            if (path == null)
            {
                throw new ArgumentNullException("path", "The path should not be empty.");
            }
        }
    }
}
