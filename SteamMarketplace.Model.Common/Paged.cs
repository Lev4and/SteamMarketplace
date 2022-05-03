namespace SteamMarketplace.Model.Common
{
    public class Paged<T>
    {
        public List<T> Items { get; private set; }

        public Pagination PageInfo { get; private set; }

        public Paged(List<T> items, int page, int limit, int totalItems)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

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

            Items = items;
            PageInfo = new Pagination(page, limit, totalItems);
        }
    }
}
