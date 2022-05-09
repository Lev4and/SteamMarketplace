namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIHttpContext
    {
        public CBRExchangeRatesHttpClient CBRExchangeRates { get; }

        public CSMoneyStoreHttpClient CSMoneyStore { get; }

        public ImportExchangeRateHttpClient ImportExchangeRate { get; }

        public ImportItemHttpClient ImportItem { get; }

        public RandomizePurchasesHttpClient RandomizePurchases { get; }

        public RandomizeSalesHttpClient RandomizeSales { get; }

        public RandomizeUsersHttpClient RandomizeUsers { get; }

        public UserInventoriesHttpClient UserInventories { get; }

        public ResourceAPIHttpContext(CBRExchangeRatesHttpClient cBRExchangeRates, 
            CSMoneyStoreHttpClient cSMoneyStore, ImportExchangeRateHttpClient importExchangeRate, 
            ImportItemHttpClient importItem, RandomizePurchasesHttpClient randomizePurchases, 
            RandomizeSalesHttpClient randomizeSales, RandomizeUsersHttpClient randomizeUsers, 
            UserInventoriesHttpClient userInventories)
        {
            CBRExchangeRates = cBRExchangeRates;
            CSMoneyStore = cSMoneyStore;
            ImportExchangeRate = importExchangeRate;
            ImportItem = importItem;
            RandomizePurchases = randomizePurchases;
            RandomizeSales = randomizeSales;
            RandomizeUsers = randomizeUsers;
            UserInventories = userInventories;
        }
    }
}
