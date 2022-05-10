using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface ISalesRepository
    {
        int GetCountSales(Guid userId);

        IQueryable<Sale> GetSales(SalesFilters filters);
    }
}
