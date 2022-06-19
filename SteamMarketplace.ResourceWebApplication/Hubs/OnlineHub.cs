namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class OnlineHub : BaseHub
    {
        public OnlineHub(ILogger<OnlineHub> logger) : base(logger)
        {

        }

        public override string ToString()
        {
            return "OnlineHub";
        }
    }
}
