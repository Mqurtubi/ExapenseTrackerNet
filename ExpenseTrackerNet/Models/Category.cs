using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTrackerNet.Enums;
namespace ExpenseTrackerNet.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; } = CategoryType.EXPENSE;
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {  get; set; }
        public long? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
