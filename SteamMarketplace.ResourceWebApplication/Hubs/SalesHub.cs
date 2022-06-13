namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class SalesHub : BaseHub
    {
        private readonly ILogger<SalesHub> _logger;

        public SalesHub(ILogger<SalesHub> logger) : base(logger)
        {
            _logger = logger;
        }

        public override string ToString()
        {
            return "SalesHub";
        }
    }
}
