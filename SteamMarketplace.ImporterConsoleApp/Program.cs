using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text;
using HttpClients = SteamMarketplace.HttpClients;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var services = new ServiceCollection()
    .AddSingleton<HttpClients.AuthorizationAPI.AuthorizationHttpClient>()
    .AddSingleton<HttpClients.AuthorizationAPI.AuthorizationAPIHttpContext>()
    .AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>()
    .AddSingleton<HttpClients.CSMoney.StoreHttpClient>()
    .AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>()
    .AddSingleton<HttpClients.ResourceAPI.CSMoneyStoreHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ImportItemHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ResourceAPIHttpContext>()
    .AddSingleton<HttpClients.HttpContext>();

var serviceProvider = services.BuildServiceProvider();

var httpContext = serviceProvider.GetService<HttpClients.HttpContext>();

httpContext.ResourceAPI.CSMoneyStore.Login("Admin", "Admin");

var stopwatch = new Stopwatch();

Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Import started");

for (var i = 0; i < 1000000; i += 50)
{
    var response = await httpContext.ResourceAPI.CSMoneyStore.GetInventoryAsync(50, i);

    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Download items from CSMoney. Total count downloaded items - {i + 50}");

    if (response?.Result?.Items == null) break;
    else
    {
        foreach (var item in response?.Result?.Items)
        {
            stopwatch.Restart();

            var responce = await httpContext.ResourceAPI.ImportItem.ImportAsync(item);

            stopwatch.Stop();

            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Duration - {stopwatch.ElapsedMilliseconds}ms " +
                $"Code - {responce.Status.Code} SteamId - {item.SteamId} FullName - {item.FullName} " +
                $"Price - {item.Price.ToString("C2", new System.Globalization.CultureInfo("en-US"))}");
        }
    }
}

Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Import completed");
Console.ReadLine();