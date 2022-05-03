namespace SteamMarketplace.Hubs.ResourceAPI
{
    public class ResourceAPIHubContext
    {
        public OnlineHubClient Online { get; }

        public ImportHubClient Import { get; }

        public AutoImportHubClient AutoImport { get; }

        public ResourceAPIHubContext(OnlineHubClient online, ImportHubClient import, AutoImportHubClient autoImport)
        {
            Online = online;
            Import = import;
            AutoImport = autoImport;
        }
    }
}
