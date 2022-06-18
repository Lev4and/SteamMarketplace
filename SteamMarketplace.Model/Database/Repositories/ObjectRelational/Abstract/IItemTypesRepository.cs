using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IItemTypesRepository : IFilterableRepository<ItemType, ItemTypesFilters>, ICRUDRepository<ItemType>
    {
        IQueryable<ItemType> GetAllItemTypes();
    }
}
