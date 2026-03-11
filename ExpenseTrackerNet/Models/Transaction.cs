using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTrackerNet.Enums;
namespace ExpenseTrackerNet.Models
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        public long Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "IDR";
        public DateTime TransactionDate { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long UserId { get; set; }
        public long CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
    }
}
