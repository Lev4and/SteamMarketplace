namespace SteamMarketplace.Model.Database.Entities
{
    public class ItemImage
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public string? Image { get; set; }

        public string? Image3d { get; set; }

        public string? SteamImg { get; set; }

        public string? Screenshot { get; set; }

        public virtual Item Item { get; set; }
    }
}
