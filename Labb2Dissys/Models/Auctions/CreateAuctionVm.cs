using System.ComponentModel.DataAnnotations;

namespace Labb2Dissys.Models.Auctions
{
    public class CreateAuctionVm
    {
        [Required]
        [StringLength(128, ErrorMessage = "Max 128 characters")]
        public string? Title { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting price must be greater than 0")]
        public decimal StartingPrice { get; set; }

        [StringLength(512, ErrorMessage = "Max 512 characters")]
        public string? Description { get; set; }

        public DateTime? EndDate { get; set; }
    }
}