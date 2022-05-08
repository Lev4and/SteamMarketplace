namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIHttpContext
    {
        public CBRExchangeRatesHttpClient CBRExchangeRates { get; }

        public CSMoneyStoreHttpClient CSMoneyStore { get; }

        public ImportExchangeRateHttpClient ImportExchangeRate { get; }

        public ImportItemHttpClient ImportItem { get; }

        public UserInventoriesHttpClient UserInventories { get; }

        public ResourceAPIHttpContext(CBRExchangeRatesHttpClient cBRExchangeRates, 
            CSMoneyStoreHttpClient cSMoneyStore, ImportExchangeRateHttpClient importExchangeRate, 
            ImportItemHttpClient importItem, UserInventoriesHttpClient userInventories)
        {
            CBRExchangeRates = cBRExchangeRates;
            CSMoneyStore = cSMoneyStore;
            ImportExchangeRate = importExchangeRate;
            ImportItem = importItem;
            UserInventories = userInventories;
        }
    }
}
