using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract
{
    public interface IItemNestedsRepository
    {
        bool Contains(Guid itemId, Guid itemNestedId);

        bool Save(ItemNested entity, bool checkOnUnique = true);

        Guid GetItemNestedId(Guid itemId, Guid itemNestedId);
    }
}
