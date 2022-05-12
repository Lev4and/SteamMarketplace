using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class DefaultDataManager
    {
        public IItemsRepository Items { get; }

        public ISalesRepository Sales { get; }

        public IUserInventoriesRepository UserInventories { get; }

        public DefaultDataManager(IItemsRepository items, ISalesRepository sales, 
            IUserInventoriesRepository userInventories)
        {
            Items = items;
            Sales = sales;
            UserInventories = userInventories;
        }
    }
}
