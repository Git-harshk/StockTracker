namespace StockTracker.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string StockSymbol { get; set; }
        public int Quantity { get; set; }

        //Foreign key
        public Guid PortfolioId { get; set; }

        //Many Transactions -> One portfolio
        public Portfolio Portfolio { get; set; }
    }
}
