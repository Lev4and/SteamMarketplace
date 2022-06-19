namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class ImportHub : BaseHub
    {
        private readonly ILogger<ImportHub> _logger;

        public ImportHub(ILogger<ImportHub> logger) : base(logger)
        {
            _logger = logger;
        }

        public override string ToString()
        {
            return "ImportHub";
        }
    }
}
