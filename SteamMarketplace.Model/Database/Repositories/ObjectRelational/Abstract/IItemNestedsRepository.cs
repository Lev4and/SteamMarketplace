using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IItemNestedsRepository
    {
        IQueryable<ItemNested> GetItemNesteds(List<Guid> itemIds);
    }
}
