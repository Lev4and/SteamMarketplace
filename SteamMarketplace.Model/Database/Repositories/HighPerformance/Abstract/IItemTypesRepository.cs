using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IItemTypesRepository
    {
        bool Contains(int cSMoneyId, out Guid id);

        bool Save(ItemType entity);

        Guid GetItemTypeId(int cSMoneyId);
    }
}
