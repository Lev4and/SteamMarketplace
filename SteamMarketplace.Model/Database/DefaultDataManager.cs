using SteamMarketplace.Model.Database.Repositories.ObjectRelational.Abstract;

namespace SteamMarketplace.Model.Database
{
    public class DefaultDataManager
    {
        public ICollectionsRepository Collections { get; }

        public ICurrenciesRepository Currencies { get; }

        public IExchangeRatesRepository ExchangeRates { get; }

        public IItemsRepository Items { get; }

        public IItemTypesRepository ItemTypes { get; }

        public IPurchasesRepository Purchases { get; }

        public IQualitiesRepository Qualities { get; }

        public IRaritiesRepository Rarities { get; }

        public ISalesRepository Sales { get; }

        public ITransactionTypesRepository TransactionTypes { get; }

        public IUserInventoriesRepository UserInventories { get; }

        public IUsersRepository Users { get; }

        public DefaultDataManager(ICollectionsRepository collections, ICurrenciesRepository currencies,
            IExchangeRatesRepository exchangeRates, IItemsRepository items, IItemTypesRepository itemTypes,
            IPurchasesRepository purchases, IQualitiesRepository qualities,
            IRaritiesRepository rarities, ISalesRepository sales, ITransactionTypesRepository transactionTypes,
            IUserInventoriesRepository userInventories, IUsersRepository users)
        {
            Collections = collections;
            Currencies = currencies;
            ExchangeRates = exchangeRates;
            Items = items;
            ItemTypes = itemTypes;
            Purchases = purchases;
            Qualities = qualities;
            Rarities = rarities;
            Sales = sales;
            TransactionTypes = transactionTypes;
            UserInventories = userInventories;
            Users = users;
        }
    }
}
