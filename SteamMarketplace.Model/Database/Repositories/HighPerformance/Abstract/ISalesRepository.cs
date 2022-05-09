using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface ISalesRepository
    {
        bool Contains(Guid itemId);

        bool Save(Sale entity, bool checkOnUnique = true);

        Guid GetSaleId(Guid itemId);

        Dictionary<Guid, Tuple<Guid, decimal>> GetRandomSales(int limit);

        void CloseSale(Guid saleId);
    }
}
