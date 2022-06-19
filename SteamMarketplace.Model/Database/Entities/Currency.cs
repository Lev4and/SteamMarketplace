using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Currency : BaseEntity
    {
        [Required]
        public string Literal { get; set; }

        [Required]
        public string CultureInfoName { get; set; }

        public virtual ICollection<ExchangeRate> Rates { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as Currency;

            return Literal == other?.Literal && CultureInfoName == other?.CultureInfoName;
        }
    }
}
