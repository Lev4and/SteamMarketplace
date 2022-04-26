using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class TypeImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public TypeImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid Import(int type)
        {
            var result = new Entities.ItemType
            {
                CSMoneyId = type
            };

            _dataManager.ItemTypes.Save(result);

            return result.Id;
        }
    }
}
