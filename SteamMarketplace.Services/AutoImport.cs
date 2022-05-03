using Microsoft.Extensions.Logging;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace SteamMarketplace.Services
{
    public class AutoImport
    {
        private readonly Stopwatch _stopwatch;
        private readonly HttpContext _httpContext;
        private readonly ILogger<AutoImport> _logger;

        public AutoImport(HttpContext httpContext, ILogger<AutoImport> logger)
        {
            _logger = logger;
            _httpContext = httpContext;
            _stopwatch = new Stopwatch();
        }

        private async IAsyncEnumerable<Item> GetItemAsync()
        {
            _httpContext.ResourceAPI.CSMoneyStore.Login("Admin", "Admin");

            while (true)
            {
                for (var i = 0; i < 1000000; i += 50)
                {
                    var response = await _httpContext.ResourceAPI.CSMoneyStore.GetInventoryAsync(50, i);

                    if (response?.Result?.Items == null) break;
                    else
                    {
                        foreach(var item in response.Result?.Items)
                        {
                            yield return item;
                        }
                    }
                }
            }
        }

        public async IAsyncEnumerable<Item> AutoImportAsync()
        {
            _httpContext.ResourceAPI.ImportItem.Login("Admin", "Admin");

            await foreach (var item in GetItemAsync())
            {
                _stopwatch.Restart();

                var responce = await _httpContext.ResourceAPI.ImportItem.ImportAsync(item);

                _stopwatch.Stop();

                if (responce.Result != Guid.Empty)
                {
                    _logger.LogInformation($"Successful item import from CS.Money ({_stopwatch.ElapsedMilliseconds}ms) " +
                        $"FullName - {item.FullName} CSMoneyId - {(long)item.Id} Price - " +
                        $"{item.Price.ToString("C2", new CultureInfo("en-US"))} Id - {responce.Result}");

                    yield return item;
                }
                else
                {
                    _logger.LogWarning($"Failed item import from CS.Money ({_stopwatch.ElapsedMilliseconds}ms) " +
                        $"FullName - {item.FullName} CSMoneyId - {(long)item.Id} Price - " +
                        $"{item.Price.ToString("C2", new CultureInfo("en-US"))} " +
                        $"The item already exists in the database");
                }
            }
        }
    }
}
