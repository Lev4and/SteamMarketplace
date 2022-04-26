using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class ItemNestedImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public ItemNestedImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid Import(Guid itemId, Guid itemNestedId)
        {
            var result = new Entities.ItemNested
            {
                ItemId = itemId,
                ItemNestedId = itemNestedId
            };

            _dataManager.ItemNesteds.Save(result, false);

            return result.Id;
        }
    }
}
