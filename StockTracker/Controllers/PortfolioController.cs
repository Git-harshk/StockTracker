using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Data;
using StockTracker.Models;
using System.Security.Claims;

namespace StockTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PortfolioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create portfolio
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var portfolio = new Portfolio
            {
                Id = Guid.NewGuid(),
                Name = name,
                UserId = userId
            };

            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            return Ok("Portfolio created");
        }

        // Get my portfolios
        [HttpGet]
        public IActionResult GetMyPortfolios()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var portfolios = _context.Portfolios
                .Where(p => p.UserId == userId)
                .ToList();

            return Ok(portfolios);
        }
    }
}
