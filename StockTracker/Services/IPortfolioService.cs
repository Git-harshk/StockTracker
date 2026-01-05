using StockTracker.DTOs;

namespace StockTracker.Services
{
    public interface IPortfolioService
    {
        PortfolioValueDto GetPortfolioValue(Guid portfolioId, Guid userId);
    }
}