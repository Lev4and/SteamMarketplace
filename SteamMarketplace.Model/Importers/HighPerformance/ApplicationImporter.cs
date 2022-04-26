using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class ApplicationImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public ApplicationImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid Import(int appId)
        {
            var result = new Entities.Application
            {
                SteamId = appId
            };

            _dataManager.Applications.Save(result);

            return result.Id;
        }
    }
}
