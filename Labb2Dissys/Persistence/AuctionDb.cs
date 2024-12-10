using System.ComponentModel.DataAnnotations;

namespace Labb2Dissys.Persistence
{
    public class AuctionDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Seller { get; set; } = string.Empty; // Email of the seller

        [Required]
        public decimal StartingPrice { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        // Navigation property for related bids
        public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
    }
}