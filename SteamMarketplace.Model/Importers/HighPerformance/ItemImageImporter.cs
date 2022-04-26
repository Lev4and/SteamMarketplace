using SteamMarketplace.Model.Database;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class ItemImageImporter
    {
        private readonly HighPerformanceDataManager _dataManager;

        public ItemImageImporter(HighPerformanceDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Guid Import(Guid itemId, string image, string image3d, string steamImg, string screenshot)
        {
            var result = new Entities.ItemImage
            {
                ItemId = itemId,
                Image = image,
                Image3d = image3d,
                SteamImg = steamImg,
                Screenshot = screenshot
            };

            _dataManager.ItemImages.Save(result, false);

            return result.Id;
        }
    }
}
