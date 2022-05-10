using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class DefaultDataManager
    {
        public ISalesRepository Sales { get; }

        public IUserInventoriesRepository UserInventories { get; }

        public DefaultDataManager(ISalesRepository sales, IUserInventoriesRepository userInventories)
        {
            Sales = sales;
            UserInventories = userInventories;
        }
    }
}
