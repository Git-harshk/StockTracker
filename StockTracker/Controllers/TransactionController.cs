using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Data;
using StockTracker.Models;

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
        public async Task<IActionResult> AddTransaction(
            Guid portfolioId,
            string stockSymbol,
            int quantity)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                PortfolioId = portfolioId,
                StockSymbol = stockSymbol,
                Quantity = quantity
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }
    }
}
