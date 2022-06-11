using Microsoft.AspNetCore.SignalR;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class BaseHub : Hub
    {
        private readonly ILogger<BaseHub> _logger;

        static Dictionary<string, ApplicationUser> Connections = new Dictionary<string, ApplicationUser>();

        public BaseHub(ILogger<BaseHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            Connections.Add(Context.ConnectionId, null);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR {ToString()} New connection {Context.ConnectionId} " +
                $"Current online {Connections.Count}");

            await Clients.All.SendAsync("OnlineChanged", Connections.Count);

            await base.OnConnectedAsync();
        }

        public async Task MatchGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR {ToString()} Connection {Context.ConnectionId} " +
                $"has been matched to the group {groupName}");
        }

        public async Task UnmatchGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR {ToString()} Connection {Context.ConnectionId} " +
                $"has been unmatched to the group {groupName}");
        }

        public async Task MatchUser(ApplicationUser user)
        {
            Connections[Context.ConnectionId] = user;

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR {ToString()} The user {user.UserName} has been matched with " +
                $"connection {Context.ConnectionId}. Current online {Connections.Count}");

            await Clients.All.SendAsync("UserConnected", user);
            await Clients.All.SendAsync("OnlineChanged", Connections.Count);
        }

        public async Task GetUsers()
        {
            _logger.LogInformation($"Microsoft.AspNetCore.SignalR {ToString()} The connection {Context.ConnectionId} " +
                $"requested a list of online users.");

            await Clients.Caller.SendAsync("OnlineUsers", Connections.Values);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connection = Connections[Context.ConnectionId];

            Connections.Remove(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR {ToString()} Lose connection {Context.ConnectionId} " +
                $"Current online {Connections.Count} Reason {exception?.Message}");

            await Clients.All.SendAsync("UserDisconnected", connection);
            await Clients.All.SendAsync("OnlineChanged", Connections.Count);

            await base.OnDisconnectedAsync(exception);
        }

        public override string ToString()
        {
            return "BaseHub";
        }
    }
}
