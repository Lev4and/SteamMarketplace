using Microsoft.Extensions.Logging;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.Entities;
using System.Globalization;

namespace SteamMarketplace.Services.Randomizers
{
    public class PurchaseRandomizer
    {
        private readonly Random _random;
        private readonly ILogger<PurchaseRandomizer> _logger;
        private readonly HighPerformanceDataManager _dataManager;

        public PurchaseRandomizer(HighPerformanceDataManager dataManager, ILogger<PurchaseRandomizer> logger)
        {
            _logger = logger;
            _random = new Random();
            _dataManager = dataManager;
        }

        private Guid GetBuyerId()
        {
            return _dataManager.ApplicationUsers.GetRandomUserId();
        }

        private Guid GetCurrencyId(Guid userId)
        {
            return _dataManager.ApplicationUsers.GetCurrencyId(userId);
        }

        private Guid GetTransactionTypeId(string ruName)
        {
            return _dataManager.TransactionTypes.GetTransactionTypeIdByRuName(ruName);
        }

        private decimal GetExchangeRate(Guid currencyId)
        {
            return _dataManager.ExchangeRates.GetRateCurrency(currencyId);
        }

        private decimal GetWalletBalance(Guid userId)
        {
            return _dataManager.ApplicationUsers.GetWalletBalance(userId);
        }

        private List<Sale> GetSales(Guid buyerId)
        {
            return _dataManager.Sales.GetRandomSales(buyerId, _random.Next(1, 101));
        }

        public void BuyItems()
        {
            var buyerId = GetBuyerId();
            var currencyId = GetCurrencyId(buyerId);
            var exchangeRate = GetExchangeRate(currencyId);

            foreach (var sale in GetSales(buyerId))
            {
                if (GetWalletBalance(buyerId) / exchangeRate >= sale.PriceUsd)
                {
                    _dataManager.Sales.CloseSale(sale.Id);
                    _dataManager.UserInventories.DeleteItemFromUserInventory(sale.SellerId, sale.ItemId);

                    var purchase = new Purchase
                    {
                        BuyerId = buyerId,
                        SaleId = sale.Id,
                        Price = sale.PriceUsd * exchangeRate,
                        PriceUsd = sale.PriceUsd,
                        PurchaseAt = DateTime.UtcNow
                    };

                    _dataManager.Purchases.Save(purchase);

                    _dataManager.ApplicationUsers.ReduceWalletBalance(buyerId, sale.PriceUsd * exchangeRate);
                    _dataManager.Transactions.Save(new Transaction
                    {
                        UserId = buyerId,
                        TypeId = GetTransactionTypeId("Покупка"),
                        PurchaseId = purchase.Id,
                        Value = sale.PriceUsd * exchangeRate,
                        HappenedAt = DateTime.UtcNow
                    });

                    _dataManager.ApplicationUsers.TopUpWalletBalance(sale.SellerId, sale.Price);
                    _dataManager.Transactions.Save(new Transaction
                    {
                        UserId = sale.SellerId,
                        TypeId = GetTransactionTypeId("Продажа"),
                        PurchaseId = purchase.Id,
                        Value = sale.Price,
                        HappenedAt = DateTime.UtcNow
                    });

                    _dataManager.UserInventories.Save(new UserInventory
                    {
                        UserId = buyerId,
                        ItemId = sale.ItemId,
                        AddedAt = DateTime.UtcNow
                    });

                    _logger.LogInformation($"User {buyerId} has purchased item {sale.ItemId} for {sale.PriceUsd.ToString("C2", new CultureInfo("us-US"))}");
                }
            }
        }
    }
}
