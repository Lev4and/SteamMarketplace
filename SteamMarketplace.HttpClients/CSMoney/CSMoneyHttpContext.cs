namespace SteamMarketplace.HttpClients.CSMoney
{
    public class CSMoneyHttpContext
    {
        public StoreHttpClient Store { get; }

        public CSMoneyHttpContext(StoreHttpClient store)
        {
            Store = store;
        }
    }
}
