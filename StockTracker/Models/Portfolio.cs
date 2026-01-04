using Microsoft.EntityFrameworkCore;

namespace StockTracker.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Foreign key
        public Guid UserId { get; set; }

        //many portfolio -> One User
        public User User { get; set; }

        //One Portfolio -> many transactions
        public ICollection<Transaction> Transactions { get; set; }
    }
}
