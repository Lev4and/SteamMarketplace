using Microsoft.AspNetCore.SignalR;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;

namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class ImportHub : Hub
    {
        private readonly ILogger<ImportHub> _logger;

        public ImportHub(ILogger<ImportHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Send(Item item)
        {
            await Clients.Others.SendAsync("Receive", item);
        }
    }
}
