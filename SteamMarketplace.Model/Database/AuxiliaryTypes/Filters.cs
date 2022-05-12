namespace SteamMarketplace.Model.Database.AuxiliaryTypes
{
    public class Filters
    {
        public Pagination Pagination { get; set; }

        public virtual void Reset()
        {
            Pagination.Reset();
        }
    }
}
