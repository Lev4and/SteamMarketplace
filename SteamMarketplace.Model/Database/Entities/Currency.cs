using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Model.Database.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }

        [Required]
        public string CultureInfoName { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
