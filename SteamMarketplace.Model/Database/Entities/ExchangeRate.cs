using System.ComponentModel.DataAnnotations.Schema;

namespace SteamMarketplace.Model.Database.Entities
{
    public class ExchangeRate : BaseEntity
    {
        public Guid CurrencyId { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Rate { get; set; }

        public DateTime DateTime { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
