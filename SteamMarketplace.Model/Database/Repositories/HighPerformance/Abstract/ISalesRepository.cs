using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface ISalesRepository
    {
        bool Contains(Guid itemId);

        bool Save(Sale entity, bool checkOnUnique = true);

        Guid GetSaleId(Guid itemId);

        List<RandomSale> GetRandomSales(Guid buyerId, int limit);

        void CloseSale(Guid saleId);
    }
}
