using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using HttpClients = SteamMarketplace.HttpClients;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var services = new ServiceCollection()
    .AddSingleton<HttpClients.Common.Services.Authorization>()
    .AddSingleton<HttpClients.AuthorizationAPI.AuthorizationHttpClient>()
    .AddSingleton<HttpClients.AuthorizationAPI.AuthorizationAPIHttpContext>()
    .AddSingleton<HttpClients.CBR.LatestHttpClient>()
    .AddSingleton<HttpClients.CBR.CBRHttpContext>()
    .AddSingleton<HttpClients.CSMoney.StoreHttpClient>()
    .AddSingleton<HttpClients.CSMoney.CSMoneyHttpContext>()
    .AddSingleton<HttpClients.ResourceAPI.CBRExchangeRatesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.CSMoneyStoreHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ImportExchangeRateHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ImportItemHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.UserInventoriesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.RandomizePurchasesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.RandomizeSalesHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.RandomizeUsersHttpClient>()
    .AddSingleton<HttpClients.ResourceAPI.ResourceAPIHttpContext>()
    .AddSingleton<HttpClients.HttpContext>();

var serviceProvider = services.BuildServiceProvider();

var httpContext = serviceProvider.GetService<HttpClients.HttpContext>();
var authorization = serviceProvider.GetService<HttpClients.Common.Services.Authorization>();

authorization.LoginByAdministrator();

var stopwatch = new Stopwatch();

var exchangeRateResponse = await httpContext.ResourceAPI.CBRExchangeRates.GetLatestExchangeRateAsync();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Download latest exchange rate");


if (exchangeRateResponse?.Result != null)
{
    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Save latest exchange rate");

    await httpContext.ResourceAPI.ImportExchangeRate.ImportAsync(exchangeRateResponse.Result);
}

while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Import started");

    for (var i = 0; i < 1000000; i += 50)
    {
        var response = await httpContext.ResourceAPI.CSMoneyStore.GetInventoryAsync(50, i);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Download items from CSMoney. " +
            $"Total count downloaded items - {i + 50}");

        if (response?.Result?.Items == null) break;
        else
        {
            foreach (var item in response?.Result?.Items)
            {
                stopwatch.Restart();

                var importResponse = await httpContext.ResourceAPI.ImportItem.ImportAsync(item);

                stopwatch.Stop();

                if (importResponse.Result != Guid.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Successful item " +
                        $"import from CS.Money ({stopwatch.ElapsedMilliseconds}ms) FullName - {item.FullName} " +
                        $"CSMoneyId - {(long)item.Id} Price - {item.Price.ToString("C2", new CultureInfo("en-US"))} " +
                        $"Id - {importResponse.Result}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Failed item " +
                        $"import from CS.Money ({stopwatch.ElapsedMilliseconds}ms) FullName - {item.FullName} " +
                        $"CSMoneyId - {(long)item.Id} Price - {item.Price.ToString("C2", new CultureInfo("en-US"))} " +
                        $"The item already exists in the database");
                }
            }
        }
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][IMPORT] Import completed");
}

Console.ReadLine();