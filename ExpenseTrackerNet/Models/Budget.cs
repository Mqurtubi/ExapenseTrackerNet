using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerNet.Models
{
    [Table("budgets")]
    public class Budget
    {
        [Key]
        public long Id { get; set; }
        public int Month {  get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public long UserId { get; set; }
        public long CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
    }
}
