namespace SteamMarketplace.Model.Database.AuxiliaryTypes
{
    public class Pagination
    {
        public int Page { get; set; }

        public int Limit { get; set; }

        public void NextPage()
        {
            Page += 1;
        }

        public void PreviousPage()
        {
            Page -= 1;
        }
    }
}