using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class CollectionImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public CollectionImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid? Import(string collection)
        {
            if (string.IsNullOrEmpty(collection))
            {
                return null; 
            }

            var result = new Entities.Collection
            {
                Name = collection
            };

            _dataManager.Collections.Save(result);

            return result.Id;
        }
    }
}
