using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class DefaultDataManager
    {
        public IItemsRepository Items { get; }

        public IPurchasesRepository Purchases { get; }

        public ISalesRepository Sales { get; }

        public IUserInventoriesRepository UserInventories { get; }

        public IUsersRepository Users { get; }

        public DefaultDataManager(IItemsRepository items, IPurchasesRepository purchases, ISalesRepository sales, 
            IUserInventoriesRepository userInventories, IUsersRepository users)
        {
            Items = items;
            Purchases = purchases;
            Sales = sales;
            UserInventories = userInventories;
            Users = users;
        }
    }
}
