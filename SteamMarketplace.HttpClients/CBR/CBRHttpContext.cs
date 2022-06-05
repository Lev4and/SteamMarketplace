namespace SteamMarketplace.HttpClients.CBR
{
    public class CBRHttpContext
    {
        public DailyHttpClient Daily { get; }

        public LatestHttpClient Latest { get; }

        public CBRHttpContext(DailyHttpClient daily, LatestHttpClient latest)
        {
            Daily = daily;
            Latest = latest;
        }
    }
}
