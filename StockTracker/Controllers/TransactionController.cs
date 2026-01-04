using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Data;
using StockTracker.DTOs;
using StockTracker.Models;
using System.Security.Claims;

namespace StockTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            Guid portfolioId,
            string stockSymbol,
            int quantity)
        {
            // 1️⃣ Get logged-in user id from JWT
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            // 2️⃣ Check portfolio belongs to this user
            var portfolio = _context.Portfolios
                .FirstOrDefault(p => p.Id == portfolioId && p.UserId == userId);

            if (portfolio == null)
                return Unauthorized("Not your portfolio");

            // 3️⃣ Create transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                StockSymbol = stockSymbol,
                Quantity = quantity,
                PortfolioId = portfolioId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var response = new TransactionResponseDto
            {
                Id = transaction.Id,
                StockSymbol = transaction.StockSymbol,
                Quantity = transaction.Quantity,
                PortfolioId = transaction.PortfolioId
            };

            return Ok(response);
        }
        [HttpGet("{portfolioId}")]
        public IActionResult GetTransactions(Guid portfolioId)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Check portfolio ownership
            var portfolio = _context.Portfolios
                .FirstOrDefault(p => p.Id == portfolioId && p.UserId == userId);

            if (portfolio == null)
                return Unauthorized("Not your portfolio");

            var transactions = _context.Transactions
                .Where(t => t.PortfolioId == portfolioId)
                .Select(t => new TransactionResponseDto
                {
                    Id = t.Id,
                    StockSymbol = t.StockSymbol,
                    Quantity = t.Quantity,
                    PortfolioId = t.PortfolioId
                })
                .ToList();

            return Ok(transactions);
        }

    }
}
