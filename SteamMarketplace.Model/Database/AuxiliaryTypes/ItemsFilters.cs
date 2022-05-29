namespace SteamMarketplace.Model.Database.AuxiliaryTypes
{
    public class ItemsFilters : Filters
    {
        public string SearchString { get; set; }

        public ItemsFilters()
        {
            SearchString = "";
        }

        public override void Reset()
        {
            SearchString = "";

            base.Reset();
        }
    }
}
