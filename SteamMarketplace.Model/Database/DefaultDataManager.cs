using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class DefaultDataManager
    {
        public IUserInventoriesRepository UserInventories { get; }

        public DefaultDataManager(IUserInventoriesRepository userInventories)
        {
            UserInventories = userInventories;
        }
    }
}
