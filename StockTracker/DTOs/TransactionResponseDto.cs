namespace StockTracker.DTOs
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public string StockSymbol { get; set; }
        public int Quantity { get; set; }
        public Guid PortfolioId { get; set; }
    }
}
