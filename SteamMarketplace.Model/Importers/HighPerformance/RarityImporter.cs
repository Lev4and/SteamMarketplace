using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class RarityImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public RarityImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid? Import(string rarity)
        {
            if (string.IsNullOrEmpty(rarity))
            {
                return null;
            }

            var result = new Entities.Rarity
            {
                Name = rarity
            };

            _dataManager.Rarities.Save(result);

            return result.Id;
        }
    }
}
