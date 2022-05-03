namespace SteamMarketplace.Hubs.HubEventArgs
{
    public class OnlineHubEventArgs : EventArgs
    {
        public int Online { get; }

        public OnlineHubEventArgs(int online)
        {
            Online = online;
        }
    }
}
