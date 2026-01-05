namespace StockTracker.Services
{
    public class MockStockPriceService : IStockPriceService
    {
        public decimal GetCurrentPrice(string stockSymbol)
        {
            // Temporary fake prices
            return stockSymbol.ToUpper() switch
            {
                "AAPL" => 180,
                "MSFT" => 320,
                "GOOG" => 140,
                _ => 100
            };
        }
    }
}