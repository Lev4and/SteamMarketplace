using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IItemsRepository
    {
        bool Contains(long cSMoneyId);

        bool Save(Item entity, bool checkOnUnique = true);

        Guid GetItemId(long cSMoneyId);

        Guid GetItemIdByFullName(string fullName);
    }
}
