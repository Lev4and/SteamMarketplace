using SteamMarketplace.Model.Marketplace.CSMoney.Types;

namespace SteamMarketplace.Hubs.ResourceAPI.ResponseModels
{
    public class ImportedItemInfo
    {
        public string UserName { get; private set; }

        public DateTime ExposedAt { get; private set; }

        public Item Item { get; private set; }

        public ImportedItemInfo(DateTime exposedAt, string userName, Item item)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            ExposedAt = exposedAt;
            UserName = userName;
            Item = item;
        }
    }
}
