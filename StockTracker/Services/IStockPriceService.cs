namespace StockTracker.Services
{
    public interface IStockPriceService
    {
        decimal GetCurrentPrice(string stockSymbol);
    }
}
