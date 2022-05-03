using Microsoft.AspNetCore.SignalR.Client;
using SteamMarketplace.Hubs.HubEventArgs;

namespace SteamMarketplace.Hubs.ResourceAPI
{
    public class OnlineHubClient : ResourceAPIHubClient
    {
        public event Action<OnlineHubEventArgs> OnlineChanged;

        public OnlineHubClient() : base(ResourceAPIRoutes.OnlinePath)
        {
            Connection.On<int>("UserConnected", (online =>
            {
                OnlineChanged?.Invoke(new OnlineHubEventArgs(online));
            }));

            Connection.On<int>("UserDisconnected", (online =>
            {
                OnlineChanged?.Invoke(new OnlineHubEventArgs(online));
            }));
        }
    }
}
