using Microsoft.Extensions.Logging;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;
using System.Globalization;

namespace SteamMarketplace.Services.Randomizers
{
    public class SaleRandomizer
    {
        private readonly Random _random;
        private readonly ILogger<SaleRandomizer> _logger;
        private readonly HighPerformanceDataManager _dataManager;

        public SaleRandomizer(HighPerformanceDataManager dataManager, ILogger<SaleRandomizer> logger)
        {
            _logger = logger;
            _random = new Random();
            _dataManager = dataManager;
        }

        private Guid GetUserId()
        {
            return _dataManager.ApplicationUsers.GetRandomUserId();
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

        private Dictionary<Guid, decimal> GetItemsFromInventory(Guid userId)
        {
            var countItems = _dataManager.UserInventories.GetCountItems(userId);

            return _dataManager.UserInventories.GetRandomItems(userId, _random.Next(1, countItems + 1));
        }

        public void ExposeItemsOnSale()
        {
            var userId = GetUserId();
            var currencyId = GetCurrencyId(userId);
            var exchangeRate = GetExchangeRate(currencyId);

            var items = GetItemsFromInventory(userId);

            foreach (var item in items)
            {
                var saleRate = GetSaleRate();

                _dataManager.Sales.Save(new Sale
                {
                    SellerId = userId,
                    ItemId = item.Key,
                    Price = item.Value * exchangeRate * saleRate,
                    PriceUsd = item.Value * saleRate,
                    ExposedAt = DateTime.UtcNow
                });

                _logger.LogInformation($"User exposed on sale item {item.Key} for {(item.Value * saleRate).ToString("C2", new CultureInfo("us-US"))}");
            }
        }
    }
}
