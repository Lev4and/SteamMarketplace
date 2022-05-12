namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIRoutes
    {
        public const string Authorization = "http://steam.marketplace.authorization.api.u1321851.plsk.regruhosting.ru/api/authorization/login/";

        //public const string Authorization = "https://localhost:7138/api/authorization/login/";

        public const string Domain = "steam.marketplace.resource.api.u1321851.plsk.regruhosting.ru/api";

        //public const string Domain = "localhost:44317/api";

        public const string CBRExchangeRatesPath = "cBR/exchangeRates/";

        public const string CBRLatestExchangeRatesQuery = "latest";

        public const string CSMoneyStorePath = "csMoney/store/";

        public const string CSMoneyStoreQuery = "inventory";

        public const string ImportExchangeRatePath = "import/exchangeRate/";

        public const string ImportExchangeRateQuery = "import";

        public const string ImportItemPath = "import/item/";

        public const string ImportItemQuery = "import";

        public const string RandomizePurchasesPath = "randomize/purchases/";

        public const string RandomizePurchasesQuery = "buyItems";

        public const string RandomizeSalesPath = "randomize/sales/";

        public const string RandomizeSalesQuery = "exposeItemsOnSale";

        public const string RandomizeUsersPath = "randomize/users/";

        public const string RandomizeUsersQuery = "create";

        public const string UserInventoriesPath = "userInventories/";

        public const string UserInventoriesQuery = "inventory";

        public const string SalesPath = "sales/";

        public const string SalesMySalesQuery = "mySales";

        public const string ItemsPath = "items/";

        public const string ItemsGroupedItemsQuery = "groupedItems";

        public const string ItemsSearchSuggestionsQuery = "searchSuggestions";
    }
}
