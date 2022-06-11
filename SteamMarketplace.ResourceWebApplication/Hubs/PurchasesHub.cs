namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class PurchasesHub : BaseHub
    {
        private readonly ILogger<PurchasesHub> _logger;

        public PurchasesHub(ILogger<PurchasesHub> logger) : base(logger)
        {
            _logger = logger;
        }

        public override string ToString()
        {
            return "PurchasesHub";
        }
    }
}
