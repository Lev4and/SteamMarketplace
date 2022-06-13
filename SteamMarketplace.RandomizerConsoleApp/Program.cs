using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text;
using HttpClients = SteamMarketplace.HttpClients;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var services = new ServiceCollection()
    .AddSingleton<HttpClients.Common.Services.Authorization>()
    .AddSingleton<HttpClients.AuthorizationAPI.AuthorizationHttpClient>()
    .AddSingleton<HttpClients.AuthorizationAPI.AuthorizationAPIHttpContext>()
    .AddSingleton<HttpClients.CBR.DailyHttpClient>()
    .AddSingleton<HttpClients.CBR.LatestHttpClient>()
    .AddSingleton<HttpClients.CBR.CBRHttpContext>()
    .AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>()
    .AddSingleton<HttpClients.CSMoney.StoreHttpClient>()
    .AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>()
    .AddSingleton<HttpClients.ResourceAPI.CBRExchangeRatesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.CSMoneyStoreHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ImportExchangeRateHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ImportItemHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ItemsHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.PurchasesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.RandomizePurchasesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.RandomizeSalesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.RandomizeUsersHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.UserInventoriesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.SalesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ResourceAPIHttpContext>()
    .AddSingleton<HttpClients.HttpContext>();

var serviceProvider = services.BuildServiceProvider();

var random = new Random();
var stopwatch = new Stopwatch();
var httpContext = serviceProvider.GetService<HttpClients.HttpContext>();
var authorization = serviceProvider.GetService<HttpClients.Common.Services.Authorization>();

authorization.LoginByAdministrator();

while (true)
{
    var task = random.Next(1, 11);

    if (task <= 4)
    {
        stopwatch.Restart();

        await httpContext.ResourceAPI.RandomizeSales.ExposeItemsOnSale();

        stopwatch.Stop();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][RANDOMIZER] The item was put up for sale within ({stopwatch.ElapsedMilliseconds}ms)");
    }

    if (task >= 5 && task <= 8)
    {
        stopwatch.Restart();

        await httpContext.ResourceAPI.RandomizePurchases.BuyItems();

        stopwatch.Stop();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][RANDOMIZER] The item was purchased within ({stopwatch.ElapsedMilliseconds}ms)");
    }

    if (task >= 9)
    {
        stopwatch.Restart();

        await httpContext.ResourceAPI.RandomizeUsers.Create();

        stopwatch.Stop();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][RANDOMIZER] New user has been created within ({stopwatch.ElapsedMilliseconds}ms)");
    }
}

Console.ReadLine();