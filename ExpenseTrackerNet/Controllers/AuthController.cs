using ExpenseTrackerNet.Data;
using ExpenseTrackerNet.DTOs;
using ExpenseTrackerNet.Models;
using ExpenseTrackerNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerNet.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController:ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (user == null) return Unauthorized("User not found");

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!validPassword) return Unauthorized("Invalid password");

            var token = _jwtService.GenerateToken(user);
            return Ok(new
            {
                token
            });
        }
    }
}
