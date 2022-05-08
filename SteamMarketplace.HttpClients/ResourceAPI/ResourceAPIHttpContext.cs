namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIHttpContext
    {
        public CBRExchangeRatesHttpClient CBRExchangeRates { get; }

        public CSMoneyStoreHttpClient CSMoneyStore { get; }

        public ImportItemHttpClient ImportItem { get; }

        public UserInventoriesHttpClient UserInventories { get; }

        public ResourceAPIHttpContext(CBRExchangeRatesHttpClient cBRExchangeRates, 
            CSMoneyStoreHttpClient cSMoneyStore, ImportItemHttpClient importItem, UserInventoriesHttpClient userInventories)
        {
            CBRExchangeRates = cBRExchangeRates;
            CSMoneyStore = cSMoneyStore;
            ImportItem = importItem;
            UserInventories = userInventories;
        }
    }
}
