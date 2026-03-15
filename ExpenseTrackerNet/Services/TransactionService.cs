using ExpenseTrackerNet.Data;
using ExpenseTrackerNet.DTOs;
using ExpenseTrackerNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerNet.Services
{
    public class TransactionService
    {
        public readonly AppDbContext _context;
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> Create(long userId, CreateTransactionDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId && (c.UserId == userId || c.IsDefault));
            if (category == null) throw new Exception("Category not found");
            var transaction = new Transaction
            {
                UserId = userId,
                CategoryId = dto.CategoryId,
                Type = dto.Type,
                Amount = dto.Amount,
                TransactionDate = dto.TransactionDate,
                PaymentMethod = dto.PaymentMethod,
                Note = dto.Note
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
        public async Task<List<Transaction>> List(long userId, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            return await _context.Transactions
                .Where(t =>
                    t.UserId == userId &&
                    t.DeletedAt == null &&
                    t.TransactionDate >= startDate &&
                    t.TransactionDate < endDate)
                .Include(t => t.Category)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        } 
    }
}
