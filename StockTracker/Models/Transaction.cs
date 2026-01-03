namespace StockTracker.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string StockSymbol { get; set; }
        public int Quantity { get; set; }
    }
}
