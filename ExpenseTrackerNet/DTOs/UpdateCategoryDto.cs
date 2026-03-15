using ExpenseTrackerNet.Enums;

namespace ExpenseTrackerNet.DTOs
{
    public class UpdateCategoryDto
    {
        public string? Name { get; set; }

        public CategoryType? Type { get; set; }

        public string? Icon { get; set; }

        public string? Color { get; set; }
        public bool IsDefault { get; set; }
    }
}
