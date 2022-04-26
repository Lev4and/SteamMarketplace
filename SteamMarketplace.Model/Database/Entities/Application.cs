namespace SteamMarketplace.Model.Database.Entities
{
    public class Application
    {
        public Guid Id { get; set; }

        public int SteamId { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
