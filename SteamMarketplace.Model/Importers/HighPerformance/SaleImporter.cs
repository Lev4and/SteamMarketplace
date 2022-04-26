using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class SaleImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public SaleImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid Import(Guid sellerId, Guid itemId, decimal price, decimal priceUsd)
        {
            var result = new Entities.Sale
            {
                SellerId = sellerId,
                ItemId = itemId,
                Price = price,
                PriceUsd = priceUsd,
                ExposedAt = DateTime.Now.ToUniversalTime(),
                SoldAt = null,
                CancelledAt = null
            };

            _dataManager.Sales.Save(result, false);

            return result.Id;
        }
    }
}
