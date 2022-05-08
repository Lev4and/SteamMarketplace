namespace SteamMarketplace.HttpClients.CBR
{
    public class CBRHttpContext
    {
        public LatestHttpClient Latest { get; }

        public CBRHttpContext(LatestHttpClient latest)
        {
            Latest = latest;
        }
    }
}
