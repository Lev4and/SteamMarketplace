using SteamMarketplace.Model.Database.AnonymousTypes;
using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ISalesRepository
    {
        int GetCountSales(Guid userId);

        int GetCountSalesItem(string fullName);

        IQueryable<Sale> GetSales(SalesFilters filters);

        IQueryable<Sale> GetSalesItem(SalesItemFilters filters);

        IQueryable<PricesDynamic> GetPricesDynamicsItem(string fullName);

        IQueryable<ExposedSalesDynamic> GetExposedSalesDynamicsItem(string fullName);
    }
}
