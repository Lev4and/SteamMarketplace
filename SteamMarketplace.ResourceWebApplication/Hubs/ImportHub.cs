using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;

namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class ImportHub : Hub
    {
        private readonly ILogger<ImportHub> _logger;

        static HashSet<string> Connections = new HashSet<string>();

        public ImportHub(ILogger<ImportHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            Connections.Add(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR ImportHub New connection {Context.ConnectionId} " +
                $"Current online {Connections.Count}");

            await Clients.All.SendAsync("Online", Connections.Count);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            Connections.Remove(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR ImportHub Lose connection {Context.ConnectionId} " +
                $"Current online {Connections.Count} Reason {exception?.Message}");

            await Clients.All.SendAsync("Online", Connections.Count);
        }
    }
}
