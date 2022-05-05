using Microsoft.Extensions.Logging;
using SteamMarketplace.HttpClients;
using SteamMarketplace.Model.Marketplace.CSMoney.Types;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using HttpClientsServices = SteamMarketplace.HttpClients.Common.Services;

namespace SteamMarketplace.Services
{
    public class AutoImport
    {
        private readonly Stopwatch _stopwatch;
        private readonly HttpContext _httpContext;
        private readonly ILogger<AutoImport> _logger;
        private readonly HttpClientsServices.Authorization _authorization;

        public AutoImport(HttpContext httpContext, ILogger<AutoImport> logger, 
            HttpClientsServices.Authorization authorization)
        {
            _logger = logger;
            _httpContext = httpContext;
            _stopwatch = new Stopwatch();
            _authorization = authorization;
        }

        private async IAsyncEnumerable<Item> GetItemAsync()
        {
            _authorization.LoginByAdministrator();

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
            _authorization.LoginByAdministrator();

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
