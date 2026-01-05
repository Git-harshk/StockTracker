using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTracker.Data;
using StockTracker.Models;
using StockTracker.Services;

namespace StockTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return BadRequest("User already exists");


            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHash = _authService.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null ||
                !_authService.VerifyPassword(password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
