using Microsoft.AspNetCore.SignalR;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class OnlineHub : Hub
    {
        private readonly ILogger<OnlineHub> _logger;

        static Dictionary<string, ApplicationUser> Connections = new Dictionary<string, ApplicationUser>();

        public OnlineHub(ILogger<OnlineHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            Connections.Add(Context.ConnectionId, null);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR OnlineHub New connection {Context.ConnectionId} " +
                $"Current online {Connections.Count}");

            await Clients.All.SendAsync("OnlineChanged", Connections.Count);

            await base.OnConnectedAsync();
        }

        public async Task MatchUser(ApplicationUser user)
        {
            Connections[Context.ConnectionId] = user;

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR OnlineHub The user {user.UserName} has been matched with " +
                $"connection {Context.ConnectionId}. Current online {Connections.Count}");

            await Clients.All.SendAsync("UserConnected", user);
            await Clients.All.SendAsync("OnlineChanged", Connections.Count);
        }

        public async Task GetUsers()
        {
            _logger.LogInformation($"Microsoft.AspNetCore.SignalR OnlineHub The connection {Context.ConnectionId} " +
                $"requested a list of online users.");

            await Clients.Caller.SendAsync("OnlineUsers", Connections.Values);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connection = Connections[Context.ConnectionId];

            Connections.Remove(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR OnlineHub Lose connection {Context.ConnectionId} " +
                $"Current online {Connections.Count} Reason {exception?.Message}");

            await Clients.All.SendAsync("UserDisconnected", connection);
            await Clients.All.SendAsync("OnlineChanged", Connections.Count);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
