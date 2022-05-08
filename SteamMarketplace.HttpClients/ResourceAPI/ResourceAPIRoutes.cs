namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIRoutes
    {
        public const string Authorization = "http://steam.marketplace.authorization.api.u1321851.plsk.regruhosting.ru/api/authorization/login/";

        //public const string Domain = "steam.marketplace.resource.api.u1321851.plsk.regruhosting.ru/api";

        public const string Domain = "localhost:5001/api";

        public const string CBRExchangeRatesPath = "cBR/exchangeRates/";

        public const string CBRLatestExchangeRatesQuery = "latest";

        public const string CSMoneyStorePath = "csMoney/store/";

        public const string CSMoneyStoreQuery = "inventory";

        public const string ImportItemPath = "import/item/";

        public const string ImportItemQuery = "import";

        public const string UserInventoriesPath = "userInventories/";

        public const string UserInventoriesQuery = "inventory";
    }
}
