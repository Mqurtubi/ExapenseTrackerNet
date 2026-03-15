using ExpenseTrackerNet.Enums;

namespace ExpenseTrackerNet.DTOs
{
    public class CreateTransactionDto
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "IDR";
        public DateTime TransactionDate { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string? Note { get; set; }
        public long CategoryId { get; set; }
    }
}
