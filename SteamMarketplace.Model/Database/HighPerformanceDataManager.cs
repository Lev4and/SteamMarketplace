using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class HighPerformanceDataManager
    {
        public IApplicationsRepository Applications { get; }

        public ICollectionsRepository Collections { get; }

        public ICurrenciesRepository Currencies { get; }

        public IItemsRepository Items { get; }

        public IItemImagesRepository ItemImages { get; }

        public IItemNestedsRepository ItemNesteds { get; }

        public IItemTypesRepository ItemTypes { get; }

        public IPurchasesRepository Purchases { get; }

        public IQualitiesRepository Qualities { get; }

        public IRaritiesRepository Rarities { get; }

        public ISalesRepository Sales { get; }

        public IUserInventoriesRepository UserInventories { get; }

        public HighPerformanceDataManager(IApplicationsRepository applications, ICollectionsRepository collections,
            ICurrenciesRepository currencies, IItemsRepository items, IItemImagesRepository itemImages,
            IItemNestedsRepository itemNesteds, IItemTypesRepository itemTypes, IPurchasesRepository purchases,
            IQualitiesRepository qualities, IRaritiesRepository rarities, ISalesRepository sales,
            IUserInventoriesRepository userInventories)
        {
            Applications = applications;
            Collections = collections;
            Currencies = currencies;
            Items = items;
            ItemImages = itemImages;
            ItemNesteds = itemNesteds;
            ItemTypes = itemTypes;
            Purchases = purchases;
            Qualities = qualities;
            Rarities = rarities;
            Sales = sales;
            UserInventories = userInventories;
        }
    }
}
