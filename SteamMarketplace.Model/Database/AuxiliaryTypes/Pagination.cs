namespace SteamMarketplace.Model.Database.AuxiliaryTypes
{
    public class Pagination
    {
        private int _page;

        public int Page
        {
            get { return _page; }
            set
            {
                _page = value;

                PageChanged?.Invoke();
            }
        }

        public int Limit { get; set; }

        public event Func<Task> PageChanged;

        public void Reset()
        {
            Page = 1;
        }
    }
}