using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class QualityImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public QualityImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid? Import(string quality)
        {
            if (string.IsNullOrEmpty(quality))
            {
                return null;
            }

            var result = new Entities.Quality
            {
                Name = quality
            };

            _dataManager.Qualities.Save(result);

            return result.Id;
        }
    }
}
