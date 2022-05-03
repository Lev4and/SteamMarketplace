using SteamMarketplace.Model.Database;
using System.Globalization;
using CSMoney = SteamMarketplace.Model.Marketplace.CSMoney.Types;
using Entities = SteamMarketplace.Model.Database.Entities;

namespace SteamMarketplace.Model.Importers.HighPerformance
{
    public class ItemImporter
    {
        private readonly HighPerformanceDataManager _dataManager;
        private readonly HighPerformanceImporterContext _importerContext;

        public ItemImporter(HighPerformanceDataManager dataManager, HighPerformanceImporterContext importerContext)
        {
            _dataManager = dataManager;
            _importerContext = importerContext;
        }

        private Entities.Item GetItem(CSMoney.Item item)
        {
            return new Entities.Item
            {
                ApplicationId = _importerContext.Application.Import(item.AppId),
                CollectionId = _importerContext.Collection.Import(item.Collection),
                QualityId = _importerContext.Quality.Import(item.Quality),
                RarityId = _importerContext.Rarity.Import(item.Rarity),
                TypeId = _importerContext.Type.Import(item.Type),
                AssetId = item.AssetId,
                CSMoneyId = (long)item.Id,
                Float = !string.IsNullOrEmpty(item.Float) ? float.Parse(item.Float, new CultureInfo("en-US").NumberFormat) : null,
                Name = item.Name,
                SteamId = item.SteamId,
                FullName = item.FullName,
                AddedAt = DateTime.Now.ToUniversalTime()
            };
        }

        private Entities.Item GetItem(Entities.Item item, CSMoney.StackItem stackItem)
        {
            return new Entities.Item
            {
                ApplicationId = item.ApplicationId,
                CollectionId = item.CollectionId,
                QualityId = item.QualityId,
                RarityId = item.RarityId,
                TypeId = item.TypeId,
                Float = item.Float,
                Name = item.Name,
                CSMoneyId = (long)stackItem.Id,
                SteamId = stackItem.SteamId,
                FullName = item.FullName,
                AddedAt = DateTime.Now.ToUniversalTime()
            };
        }

        private void SaveItemImage(Guid itemId, string image, string image3d, string steamImg, string screenshot)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException("itemId", "The item id must not be empty.");
            }

            _importerContext.ItemImage.Import(itemId, image, image3d, steamImg, screenshot);
        }

        private void SaveStickers(Guid itemId, List<CSMoney.Sticker> stickers)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException("itemId", "The item id must not be empty.");
            }

            if (stickers != null)
            {
                foreach (var sticker in stickers)
                {
                    if (sticker != null)
                    {
                        var stickerId = _dataManager.Items.GetItemIdByFullName(sticker.Name);

                        if (stickerId != Guid.Empty) _importerContext.ItemNested.Import(itemId, stickerId);
                    }
                }
            }
        }

        private void SaveStackItems(Entities.Item item, CSMoney.Item cSMoneyItem, bool withStack = true)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "The item must not be empty.");
            }

            if (cSMoneyItem == null)
            {
                throw new ArgumentNullException("cSMoneyItem", "The Cs.Money item must not be empty.");
            }

            if (withStack)
            {
                if (cSMoneyItem.StackItems != null)
                {
                    foreach (var stackItem in cSMoneyItem.StackItems)
                    {
                        if (!_dataManager.Items.Contains((long)stackItem.Id))
                        {
                            SaveItem(GetItem(item, stackItem), cSMoneyItem, false);
                        }
                    }
                }
            }
        }

        private void AddInInventory(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException("itemId", "The item id must not be empty.");
            }

            _importerContext.UserInventory.Import(Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"), itemId);
        }

        private void PutUpForSale(Guid itemId, double price)
        {
            if (itemId == Guid.Empty)
            {
                throw new ArgumentNullException("itemId", "The item id must not be empty.");
            }

            _importerContext.Sale.Import(Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"), itemId,
                Convert.ToDecimal(price), Convert.ToDecimal(price));
        }

        private Guid SaveItem(Entities.Item item, CSMoney.Item cSMoneyItem, bool withStack = true)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "The item must not be empty.");
            }

            if (cSMoneyItem == null)
            {
                throw new ArgumentNullException("cSMoneyItem", "The Cs.Money item must not be empty.");
            }

            _dataManager.Items.Save(item, false);

            SaveItemImage(item.Id, cSMoneyItem.Img, cSMoneyItem._3d, cSMoneyItem.SteamImg, cSMoneyItem.Screenshot);

            SaveStickers(item.Id, cSMoneyItem.Stickers);

            AddInInventory(item.Id);

            PutUpForSale(item.Id, cSMoneyItem.Price);

            SaveStackItems(item, cSMoneyItem, withStack);

            return item.Id;
        }

        public Guid Import(CSMoney.Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "The item must not be empty.");
            }

            if (!_dataManager.Items.Contains((long)item.Id))
            {
                return SaveItem(GetItem(item), item);
            }

            return Guid.Empty;
        }
    }
}
