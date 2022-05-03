using SteamMarketplace.Hubs.ResourceAPI.ResponseModels;

namespace SteamMarketplace.Hubs.HubEventArgs
{
    public class ImportHubEventArgs : EventArgs
    {
        public ImportedItemInfo ImportedItemInfo { get; }

        public ImportHubEventArgs(ImportedItemInfo importedItemInfo)
        {
            if (importedItemInfo == null)
            {
                throw new ArgumentNullException("importedItemInfo");
            }

            ImportedItemInfo = importedItemInfo;
        }
    }
}
