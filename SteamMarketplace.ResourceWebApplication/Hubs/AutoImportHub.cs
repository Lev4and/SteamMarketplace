using Microsoft.AspNetCore.SignalR;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using SteamMarketplace.Services;
using System.Runtime.CompilerServices;

namespace SteamMarketplace.ResourceWebApplication.Hubs
{
    public class AutoImportHub : Hub
    {
        private readonly AutoImport _autoImport;
        private readonly ILogger<AutoImportHub> _logger;

        static HashSet<string> Connections = new HashSet<string>();

        public AutoImportHub(AutoImport autoImport, ILogger<AutoImportHub> logger)
        {
            _logger = logger;
            _autoImport = autoImport;
        }

        public override async Task OnConnectedAsync()
        {
            Connections.Add(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR AutoImportHub New connection {Context.ConnectionId} " +
                $"Current online {Connections.Count}");

            await Clients.All.SendAsync("UserConnected", Connections.Count);
            await base.OnConnectedAsync();
        }

        public async IAsyncEnumerable<Item> Streaming()
        {
            await foreach (var item in _autoImport.AutoImportAsync())
            {
                yield return item;
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Connections.Remove(Context.ConnectionId);

            _logger.LogInformation($"Microsoft.AspNetCore.SignalR AutoImportHub Lose connection {Context.ConnectionId} " +
                $"Current online {Connections.Count} Reason {exception?.Message}");

            await Clients.All.SendAsync("UserDisconnected", Connections.Count);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
