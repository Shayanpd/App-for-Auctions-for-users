using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2Dissys.Persistence
{
    public class BidDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Bidder { get; set; } = string.Empty; // Email of the bidder

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        // Foreign Key and Navigation Property
        [ForeignKey("AuctionId")]
        public AuctionDb AuctionDb { get; set; }

        public int AuctionId { get; set; }
    }
}