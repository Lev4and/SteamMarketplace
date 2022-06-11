using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SteamMarketplace.Model.Database;
using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.Entities;
using SteamMarketplace.ResourceWebApplication.Hubs;
using System.Globalization;

namespace SteamMarketplace.Services.Randomizers
{
    public class PurchaseRandomizer
    {
        private readonly Random _random;
        private readonly IHubContext<SalesHub> _salesHub;
        private readonly ILogger<PurchaseRandomizer> _logger;
        private readonly IHubContext<PurchasesHub> _purchasesHub;
        private readonly HighPerformanceDataManager _dataManager;

        public PurchaseRandomizer(HighPerformanceDataManager dataManager, IHubContext<PurchasesHub> purchasesHub,
            IHubContext<SalesHub> salesHub, ILogger<PurchaseRandomizer> logger)
        {
            _logger = logger;
            _salesHub = salesHub;
            _random = new Random();
            _dataManager = dataManager;
            _purchasesHub = purchasesHub;
        }

        private Guid GetTransactionTypeId(string ruName)
        {
            return _dataManager.TransactionTypes.GetTransactionTypeIdByRuName(ruName);
        }

        private decimal GetExchangeRate(Guid currencyId)
        {
            return _dataManager.ExchangeRates.GetRateCurrency(currencyId);
        }

        private ApplicationUser GetBuyer()
        {
            return _dataManager.ApplicationUsers.GetRandomUser();
        }

        private List<RandomSale> GetSales(Guid buyerId)
        {
            return _dataManager.Sales.GetRandomSales(buyerId, _random.Next(1, 101));
        }

        public async Task BuyItemsAsync()
        {
            var buyer = GetBuyer();
            var exchangeRate = GetExchangeRate(buyer.CurrencyId);

            foreach (var sale in GetSales(buyer.Id))
            {
                if (buyer.WalletBalance / exchangeRate >= sale.PriceUsd)
                {
                    _dataManager.Sales.CloseSale(sale.Id);

                    await _salesHub.Clients.Group($"{sale.Id}").SendAsync("SaleClosed", sale);
                    await _salesHub.Clients.Group($"{sale.SellerId}").SendAsync("SaleClosed", sale);
                    await _salesHub.Clients.Group($"{sale.ItemId}").SendAsync("CertainItemSaleClosed", sale);
                    await _salesHub.Clients.Group($"{sale.ItemFullName}").SendAsync("CertainItemSaleClosed", sale);

                    _dataManager.UserInventories.DeleteItemFromUserInventory(sale.SellerId, sale.ItemId);

                    var purchase = new Purchase
                    {
                        BuyerId = buyer.Id,
                        SaleId = sale.Id,
                        Price = sale.PriceUsd * exchangeRate,
                        PriceUsd = sale.PriceUsd,
                        PurchaseAt = DateTime.UtcNow
                    };

                    _dataManager.Purchases.Save(purchase);

                    _dataManager.ApplicationUsers.ReduceWalletBalance(buyer.Id, sale.PriceUsd * exchangeRate);
                    _dataManager.Transactions.Save(new Transaction
                    {
                        UserId = buyer.Id,
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
                        UserId = buyer.Id,
                        ItemId = sale.ItemId,
                        AddedAt = DateTime.UtcNow
                    });

                    await _purchasesHub.Clients.All.SendAsync("ItemPurchased", buyer, sale, purchase);

                    await _purchasesHub.Clients.Group($"{sale.ItemId}").SendAsync("CertainItemPurchased", buyer, sale, purchase);
                    await _purchasesHub.Clients.Group($"{sale.SellerId}").SendAsync("CertainItemPurchased", buyer, sale, purchase);
                    await _purchasesHub.Clients.Group($"{sale.ItemFullName}").SendAsync("CertainItemPurchased", buyer, sale, purchase);

                    _logger.LogInformation($"User {buyer} has purchased item {sale.ItemId} for {sale.PriceUsd.ToString("C2", new CultureInfo("us-US"))}");
                }
            }
        }
    }
}
