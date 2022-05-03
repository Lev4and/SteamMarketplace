namespace SteamMarketplace.Model.Common
{
    public class Pagination
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        public int PagesCount { get; set; }

        public int TotalItems { get; set; }

        public Pagination(int page, int limit, int totalItems)
        {
            if (page < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            if (limit < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(limit));
            }

            if (totalItems < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalItems));
            }

            Page = page; 
            Limit = limit;
            TotalItems = totalItems;

            PagesCount = totalItems > 0 && limit > 0 ? Convert.ToInt32(Math.Round((double)totalItems / (double)limit, 0)) : 0;
        }
    }
}
