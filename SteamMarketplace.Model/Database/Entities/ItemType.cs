namespace SteamMarketplace.Model.Database.Entities
{
    public class ItemType
    {
        public Guid Id { get; set; }

        public int CSMoneyId { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
