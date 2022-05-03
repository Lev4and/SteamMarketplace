using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class OnlineHub : Hub
    {
        private readonly ILogger<OnlineHub> _logger;

        static HashSet<string> Connections = new HashSet<string>();

        public OnlineHub(ILogger<OnlineHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            Connections.Add(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR OnlineHub New connection {Context.ConnectionId} " +
                $"Current online {Connections.Count}");

            await Clients.All.SendAsync("UserConnected", Connections.Count);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Connections.Remove(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR OnlineHub Lose connection {Context.ConnectionId} " +
                $"Current online {Connections.Count} Reason {exception?.Message}");

            await Clients.All.SendAsync("UserDisconnected", Connections.Count);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
