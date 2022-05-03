using SteamMarketplace.Hubs.ResourceAPI;

namespace SteamMarketplace.Hubs
{
    public class HubContext
    {
        public ResourceAPIHubContext ResourceAPI { get; }

        public HubContext(ResourceAPIHubContext resourceAPI)
        {
            ResourceAPI = resourceAPI;
        }
    }
}
