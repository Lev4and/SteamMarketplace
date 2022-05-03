using SteamMarketplace.Model.Database.AuxiliaryTypes;
using SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract
{
    public interface IUserInventoriesRepository
    {
        int GetCountItemsInUserInventory(Guid userId);

        IQueryable<UserInventory> GetUserInventory(UserInventoriesFilters filters);
    }
}
