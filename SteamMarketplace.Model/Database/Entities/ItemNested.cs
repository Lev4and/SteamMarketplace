namespace SteamMarketplace.Model.Database.Entities
{
    public class ItemNested
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public Guid ItemNestedId { get; set; }

        public virtual Item Item { get; set; }

        public virtual Item Nested { get; set; }
    }
}
