using System.Collections.ObjectModel;

namespace SteamMarketplace.Model.Common
{
    public class Pagination
    {
        private int _pageCount;
        private readonly int _leftSideCount;
        private readonly int _rightSideCount;
        private readonly int _centerSideCount;

        public int Page { get; set; }

        public int Limit { get; set; }

        public int PagesCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;

                Generate();
            }
        }

        public int TotalItems { get; set; }

        public ObservableCollection<int> Items { get; private set; } = new ObservableCollection<int>();

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

            _leftSideCount = 1;
            _rightSideCount = 1;
            _centerSideCount = 2;

            Page = page; 
            Limit = limit;
            TotalItems = totalItems;
            PagesCount = totalItems > 0 && limit > 0 ? Convert.ToInt32(Math.Ceiling((double)totalItems / (double)limit)) : 0;
        }

        private List<int> GetRange(int start, int end)
        {
            var result = new List<int>();
            var length = Math.Abs(end - start) + 1;

            for (var i = 0; i < length; i++)
            {
                result.Add(i * Math.Sign(end - start) + start);
            }

            return result;
        }

        private void Generate()
        {
            Items.Clear();

            var centerLeft = 0;
            var centerRight = 0;

            SetItems(GetRange(1, _leftSideCount));

            if (PagesCount >= 3)
            {
                centerLeft = Math.Max(_leftSideCount + 1, Page - _centerSideCount);
                centerRight = Math.Min(PagesCount - _rightSideCount, centerLeft + _centerSideCount * 2);
                centerLeft = Math.Max(_leftSideCount + 1, centerRight - _centerSideCount * 2);

                SetItems(GetRange(centerLeft, centerRight));
            }

            if (PagesCount > 1)
            {
                SetItems(GetRange(PagesCount - _rightSideCount + 1, PagesCount));
            }
        }

        private void SetItems(List<int> range)
        {
            foreach (var item in range)
            {
                if (!Items.Contains(item))
                {
                    Items.Add(item);
                }
            }
        }
    }
}
