using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Model.Database.Entities
{
    public class TransactionType
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string RuName { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
