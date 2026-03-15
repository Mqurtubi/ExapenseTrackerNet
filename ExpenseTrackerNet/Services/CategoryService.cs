using ExpenseTrackerNet.Data;
using ExpenseTrackerNet.DTOs;
using ExpenseTrackerNet.Models;
using ExpenseTrackerNet.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerNet.Services
{ 
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService (AppDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Create(long userId, CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Type = dto.Type,
                Color = dto.Color,
                Icon = dto.Icon,
                IsDefault = dto.IsDefault,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> List(long userId, CategoryType? type )
        {
            var query = _context.Categories.Where(c=> c.IsDefault || c.UserId  == userId);
            if(type != null)
            {
                query = query.Where(c => c.Type == type || c.Type == CategoryType.BOTH);
            }
            return await query.OrderBy(c=>c.Name).ToListAsync();
        }

        public async Task<Category> Update(long userId, long categoryId, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
            if (category == null) throw new Exception("Category not found");
            if (category.IsDefault) throw new Exception("Category default cannot be changed");
            if (dto.Type != null && dto.Type != category.Type)
            {
                bool used = await _context.Transactions.AnyAsync(t => t.CategoryId == categoryId);
                if (used) throw new Exception("Category type cannot be changed because it is used by transaction");
            }
            if (dto.Name != null) category.Name = dto.Name;
            if (dto.Type != null) category.Type = dto.Type.Value;
            if (dto.Color != null) category.Color = dto.Color;
            if (dto.Icon != null) category.Icon = dto.Icon;

            category.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return category;
        }
        public async Task Delete(long userId, long categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == categoryId);
            if (category == null) throw new Exception("Category not found");
            if (category.IsDefault)throw new Exception("default category cannot be deleted");
            bool used = await _context.Transactions.AnyAsync(t => t.CategoryId == categoryId);

            if (used) throw new Exception("Category is used by transaction and cannot be deleted");

            _context.Categories.Remove(category);
             await _context.SaveChangesAsync();
        }
    }
}
