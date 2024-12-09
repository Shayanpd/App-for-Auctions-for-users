using System.ComponentModel.DataAnnotations;
using Labb2Dissys.Core;

namespace Labb2Dissys.Models.Auctions
{
    public class AuctionDetailsVm
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Seller")]
        [Required]
        public string Seller { get; set; } = string.Empty;

        [Display(Name = "Starting Price")]
        [DataType(DataType.Currency)]
        public decimal StartingPrice { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EndDate { get; set; }

        public bool IsActive => EndDate > DateTime.Now;

        [Display(Name = "Highest Bid")]
        [DataType(DataType.Currency)]
        public decimal HighestBid => Bids.Any() ? Bids.Max(b => b.Amount) : StartingPrice;

        public List<BidVm> Bids { get; set; } = new();

        public static AuctionDetailsVm FromAuction(Auction auction)
        {
            if (auction == null) throw new ArgumentNullException(nameof(auction));
            return new AuctionDetailsVm
            {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                Seller = auction.Seller,
                StartingPrice = auction.StartingPrice,
                EndDate = auction.EndDate,
                Bids = auction.Bids?.Select(BidVm.FromBid).ToList() ?? new List<BidVm>()
            };
        }
    }
}