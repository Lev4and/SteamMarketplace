namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class UsersHub : BaseHub
    {
        private readonly ILogger<UsersHub> _logger;

        public UsersHub(ILogger<UsersHub> logger) : base(logger)
        {
            _logger = logger;
        }

        public override string ToString()
        {
            return "UsersHub";
        }
    }
}
