using SteamMarketplace.Model.Importers.HighPerformance;

namespace SteamMarketplace.Model.Importers
{
    public class HighPerformanceImporterContext
    {
        public ApplicationImporter Application { get; }

        public CollectionImporter Collection { get; }

        public ExchangeRateImporter ExchangeRate { get; }

        public ItemImageImporter ItemImage { get; }

        public ItemNestedImporter ItemNested { get; }

        public QualityImporter Quality { get; }

        public RarityImporter Rarity { get; }

        public SaleImporter Sale { get; }

        public TypeImporter Type { get; }

        public UserInventoryImporter UserInventory { get; }

        public HighPerformanceImporterContext(ApplicationImporter application, CollectionImporter collection,
            ExchangeRateImporter exchangeRate, ItemImageImporter itemImage, ItemNestedImporter itemNested, QualityImporter quality,
            RarityImporter rarity, SaleImporter sale, TypeImporter type, UserInventoryImporter userInventory)
        {
            Application = application;
            Collection = collection;
            ExchangeRate = exchangeRate;
            ItemImage = itemImage;
            ItemNested = itemNested;
            Quality = quality;
            Rarity = rarity;
            Sale = sale;
            Type = type;
            UserInventory = userInventory;
        }
    }
}
