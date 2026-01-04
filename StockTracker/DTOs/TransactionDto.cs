namespace StockTracker.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string StockSymbol { get; set; }
        public int Quantity { get; set; }
    }
}
