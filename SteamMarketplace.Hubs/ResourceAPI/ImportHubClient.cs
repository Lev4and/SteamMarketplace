using Microsoft.AspNetCore.SignalR.Client;
using SteamMarketplace.Hubs.HubEventArgs;
using SteamMarketplace.Hubs.ResourceAPI.ResponseModels;

namespace SteamMarketplace.Hubs.ResourceAPI
{
    public class ImportHubClient : ResourceAPIHubClient
    {
        public event Action<ImportHubEventArgs> ItemImported;

        public event Action<OnlineHubEventArgs> OnlineChanged;

        public ImportHubClient() : base(ResourceAPIRoutes.ImportPath)
        {
            Connection.On<ImportedItemInfo>("ItemImported", (importedItemInfo) =>
            {
                ItemImported?.Invoke(new ImportHubEventArgs(importedItemInfo));
            });

            Connection.On<int>("Online", (online) =>
            {
                OnlineChanged?.Invoke(new OnlineHubEventArgs(online));
            });
        }
    }
}
