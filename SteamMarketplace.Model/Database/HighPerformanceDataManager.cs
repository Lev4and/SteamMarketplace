using SteamMarketplace.Model.Database.Repositories.HighPerformance.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class HighPerformanceDataManager
    {
        public IApplicationsRepository Applications { get; }

        public IApplicationUsersRepository ApplicationUsers { get; }

        public IApplicatonRolesRepository ApplicatonRoles { get; }

        public ICollectionsRepository Collections { get; }

        public ICurrenciesRepository Currencies { get; }

        public IExchangeRatesRepository ExchangeRates { get; }

        public IItemsRepository Items { get; }

        public IItemImagesRepository ItemImages { get; }

        public IItemNestedsRepository ItemNesteds { get; }

        public IItemTypesRepository ItemTypes { get; }

        public IPurchasesRepository Purchases { get; }

        public IQualitiesRepository Qualities { get; }

        public IRaritiesRepository Rarities { get; }

        public ISalesRepository Sales { get; }

        public ITransactionsRepository Transactions { get; }

        public ITransactionTypesRepository TransactionTypes { get; }

        public IUserInventoriesRepository UserInventories { get; }

        public IUserRolesRepository UserRoles { get; }

        public HighPerformanceDataManager(IApplicationsRepository applications, 
            IApplicationUsersRepository applicationUsers, IApplicatonRolesRepository applicatonRoles, 
            ICollectionsRepository collections, ICurrenciesRepository currencies, 
            IExchangeRatesRepository exchangeRates, IItemsRepository items, IItemImagesRepository itemImages,
            IItemNestedsRepository itemNesteds, IItemTypesRepository itemTypes, IPurchasesRepository purchases,
            IQualitiesRepository qualities, IRaritiesRepository rarities, ISalesRepository sales, 
            ITransactionsRepository transactions, ITransactionTypesRepository transactionTypes, 
            IUserInventoriesRepository userInventories, IUserRolesRepository userRoles)
        {
            Applications = applications;
            ApplicationUsers = applicationUsers;
            ApplicatonRoles = applicatonRoles;
            Collections = collections;
            Currencies = currencies;
            ExchangeRates = exchangeRates;
            Items = items;
            ItemImages = itemImages;
            ItemNesteds = itemNesteds;
            ItemTypes = itemTypes;
            Purchases = purchases;
            Qualities = qualities;
            Rarities = rarities;
            Sales = sales;
            Transactions = transactions;
            TransactionTypes = transactionTypes;
            UserInventories = userInventories;
            UserRoles = userRoles;
        }
    }
}
