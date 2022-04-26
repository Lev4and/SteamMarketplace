using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IPurchasesRepository
    {
        bool Contains(Guid saleId);

        bool Save(Purchase entity, bool checkOnUnique = true);

        Guid GetPurchaseId(Guid saleId);
    }
}
