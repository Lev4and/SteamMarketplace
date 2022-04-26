namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIHttpContext
    {
        public CSMoneyStoreHttpClient CSMoneyStore { get; }

        public ImportItemHttpClient ImportItem { get; }

        public ResourceAPIHttpContext(CSMoneyStoreHttpClient cSMoneyStore, ImportItemHttpClient importItem)
        {
            CSMoneyStore = cSMoneyStore;
            ImportItem = importItem;
        }
    }
}
