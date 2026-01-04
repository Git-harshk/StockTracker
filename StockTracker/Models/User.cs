using System.ComponentModel.DataAnnotations;

namespace StockTracker.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // one to many 
        public ICollection<Portfolio> Portfolios { get; set; }
    }
}
