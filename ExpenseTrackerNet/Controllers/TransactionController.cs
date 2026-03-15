using ExpenseTrackerNet.DTOs;
using ExpenseTrackerNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerNet.Controllers
{
    [ApiController]
    [Authorize]
    [Route("transaction")]
    public class TransactionController:ControllerBase
    {
        public readonly TransactionService _service;
        public TransactionController(TransactionService service)
        {
            _service = service;
        }
        private long GetUserId()
        {
            return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionDto dto)
        {
            var transaction = await _service.Create(GetUserId(), dto);
            return StatusCode(201, new
            {
                message = "transaction created",
                data = new
                {
                    id = transaction.Id,
                    type = transaction.Type,
                    amount = transaction.Amount,
                    payment_method = transaction.PaymentMethod,
                    note = transaction.Note,
                    transactio_date = transaction.TransactionDate,
                    category_id = transaction.CategoryId,
                    user_id = transaction.UserId,
                }
            });
        }
    }
}
