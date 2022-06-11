using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.ResourceWebApplication.Hubs;
using System.Globalization;

namespace SteamMarketplace.Services.Randomizers
{
    public class SaleRandomizer
    {
        private readonly Random _random;
        private readonly IHubContext<SalesHub> _hub;
        private readonly ILogger<SaleRandomizer> _logger;
        private readonly HighPerformanceDataManager _dataManager;

        public SaleRandomizer(HighPerformanceDataManager dataManager, IHubContext<SalesHub> hub, 
            ILogger<SaleRandomizer> logger)
        {
            _hub = hub;
            _logger = logger;
            _random = new Random();
            _dataManager = dataManager;
        }

        private Guid GetCurrencyId(Guid userId)
        {
            return _dataManager.ApplicationUsers.GetCurrencyId(userId);
        }

        private decimal GetSaleRate()
        {
            return Convert.ToDecimal(_random.Next(90, 111)) / Convert.ToDecimal(100);
        }

        private decimal GetExchangeRate(Guid currencyId)
        {
            return _dataManager.ExchangeRates.GetRateCurrency(currencyId);
        }

        private ApplicationUser GetUser()
        {
            return _dataManager.ApplicationUsers.GetRandomUser();
        }

        private List<RandomUserInventoryItem> GetItemsFromInventory(Guid userId)
        {
            var countItems = _dataManager.UserInventories.GetCountItems(userId);

            return _dataManager.UserInventories.GetRandomItems(userId, _random.Next(1, countItems + 1));
        }

        public async Task ExposeItemsOnSaleAsync()
        {
            var user = GetUser();
            var exchangeRate = GetExchangeRate(user.CurrencyId);

            var items = GetItemsFromInventory(user.Id);

            foreach (var item in items)
            {
                var saleRate = GetSaleRate();
                var sale = new Sale()
                {
                    SellerId = user.Id,
                    ItemId = item.ItemId,
                    Price = item.PriceUsd * exchangeRate * saleRate,
                    PriceUsd = item.PriceUsd * saleRate,
                    ExposedAt = DateTime.UtcNow
                };

                _dataManager.Sales.Save(sale);

                await _hub.Clients.All.SendAsync("ItemExposedOnSale", item, sale);

                await _hub.Clients.Group($"{sale.ItemId}").SendAsync("CertainItemExposedOnSale", item, sale);
                await _hub.Clients.Group($"{item.ItemFullName}").SendAsync("CertainItemExposedOnSale", item, sale);
                await _hub.Clients.Group($"{sale.SellerId}").SendAsync("SellerExposedOnSale", item, sale);

                _logger.LogInformation($"User exposed on sale item {item.ItemId} for {(item.PriceUsd * saleRate).ToString("C2", new CultureInfo("us-US"))}");
            }
        }
    }
}
