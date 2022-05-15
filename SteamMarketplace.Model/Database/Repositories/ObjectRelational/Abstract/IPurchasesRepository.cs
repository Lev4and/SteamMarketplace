using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IPurchasesRepository
    {
        int GetCountPurchases(Guid userId);

        IQueryable<Purchase> GetPurchases(PurchasesFilters filters);
    }
}
