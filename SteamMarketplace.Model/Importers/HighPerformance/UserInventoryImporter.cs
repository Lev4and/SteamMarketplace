using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class UserInventoryImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public UserInventoryImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid Import(Guid userId, Guid itemId)
        {
            var result = new Entities.UserInventory
            {
                UserId = userId,
                ItemId = itemId
            };

            _dataManager.UserInventories.Save(result, false);

            return result.Id;
        }
    }
}
