namespace SteamMarketplace.HttpClients.ResourceAPI
{
    public class ResourceAPIHttpContext
    {
        public CSMoneyStoreHttpClient CSMoneyStore { get; }

        public ImportItemHttpClient ImportItem { get; }

        public UserInventoriesHttpClient UserInventories { get; }

        public ResourceAPIHttpContext(CSMoneyStoreHttpClient cSMoneyStore, ImportItemHttpClient importItem,
            UserInventoriesHttpClient userInventories)
        {
            CSMoneyStore = cSMoneyStore;
            ImportItem = importItem;
            UserInventories = userInventories;
        }
    }
}
