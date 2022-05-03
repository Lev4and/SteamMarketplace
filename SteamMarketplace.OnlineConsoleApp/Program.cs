using Microsoft.Extensions.DependencyInjection;
using Hubs = SteamMarketplace.Hubs;
using System.Text;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var services = new ServiceCollection()
    .AddSingleton<Hubs.ResourceAPI.ImportHubClient>()
    .AddSingleton<Hubs.ResourceAPI.OnlineHubClient>()
    .AddSingleton<Hubs.ResourceAPI.AutoImportHubClient>()
    .AddSingleton<Hubs.ResourceAPI.ResourceAPIHubContext>()
    .AddSingleton<Hubs.HubContext>();

var serviceProvider = services.BuildServiceProvider();

var hubContext = serviceProvider.GetService<Hubs.HubContext>();

hubContext.ResourceAPI.Online.OnlineChanged += (args) =>
{
    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][ONLINE] {args.Online}");
};

await hubContext.ResourceAPI.Online.Connect();

Console.ReadLine();