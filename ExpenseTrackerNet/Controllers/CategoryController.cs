using ExpenseTrackerNet.DTOs;
using ExpenseTrackerNet.Enums;
using ExpenseTrackerNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace ExpenseTrackerNet.Controllers
{
    [ApiController]
    [Route("categories")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;
        public CategoryController(CategoryService service)
        {
            _service = service;
        }
        private long GetUserID()
        {
            return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var category = await _service.Create(GetUserID(), dto);
            return StatusCode(201, new
            {
                message = "category created",
                data = new
                {
                    id = category.Id,
                    name = category.Name,
                    type = category.Type,
                    icon = category.Icon,
                    color = category.Color,
                    isDefault = category.IsDefault,
                    created_at = category.CreatedAt
                }
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryType? type)
        {
            var categories = await _service.List(GetUserID(), type);
            return Ok(new
            {
                data = categories.Select(c => new
                {
                    id = c.Id,
                    name = c.Name,
                    type = c.Type,
                    icon = c.Icon,
                    color = c.Color,
                    is_default = c.IsDefault
                })
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateCategoryDto dto)
        {
            var category = await _service.Update(GetUserID(), id, dto);
            return Ok(new
            {
                message = "category updated",
                data = new
                {
                    id = category.Id,
                    name = category.Name,
                    type = category.Type,
                    icon = category.Icon,
                    color = category.Color,
                    is_default = category.IsDefault
                }
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.Delete(GetUserID(),id);
            return Ok(new
            {
                message = "category deleted"
            });
        }
    }
}
