using StockTracker.Data;
using StockTracker.DTOs;

namespace StockTracker.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockPriceService _priceService;

        public PortfolioService(
            ApplicationDbContext context,
            IStockPriceService priceService)
        {
            _context = context;
            _priceService = priceService;
        }

        public PortfolioValueDto GetPortfolioValue(Guid portfolioId, Guid userId)
        {
            var portfolio = _context.Portfolios
                .FirstOrDefault(p => p.Id == portfolioId && p.UserId == userId);

            if (portfolio == null)
                throw new Exception("Portfolio not found");

            var transactions = _context.Transactions
                .Where(t => t.PortfolioId == portfolioId)
                .ToList();

            decimal totalValue = 0;

            foreach (var t in transactions)
            {
                var price = _priceService.GetCurrentPrice(t.StockSymbol);
                totalValue += price * t.Quantity;
            }

            return new PortfolioValueDto
            {
                PortfolioId = portfolioId,
                TotalValue = totalValue
            };
        }
    }
}