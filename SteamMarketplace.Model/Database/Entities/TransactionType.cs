using System.ComponentModel.DataAnnotations;

namespace SteamMarketplace.Model.Database.Entities
{
    public class TransactionType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string RuName { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as TransactionType;

            return Name == other?.Name && RuName == other?.RuName;
        }
    }
}
