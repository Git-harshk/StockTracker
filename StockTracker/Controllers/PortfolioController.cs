using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Data;
using StockTracker.DTOs;
using StockTracker.Models;
using StockTracker.Services;
using System.Security.Claims;

namespace StockTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(
            ApplicationDbContext context,
            IPortfolioService portfolioService)
        {
            _context = context;
            _portfolioService = portfolioService;
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
                .Select(p => new PortfolioDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

            return Ok(portfolios);
        }

        [HttpGet("{portfolioId}/value")]
        public IActionResult GetPortfolioValue(Guid portfolioId)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var value = _portfolioService.GetPortfolioValue(portfolioId, userId);

            return Ok(value);
        }
    }
}
