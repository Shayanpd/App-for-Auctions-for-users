using System;
using System.ComponentModel.DataAnnotations;
using Labb2Dissys.Core;

namespace Labb2Dissys.Models.Auctions
{
    public class WonAuctionVm
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EndDate { get; set; }

        // Property for Highest Bid
        [Display(Name = "Highest Bid")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal HighestBid { get; set; }

        public static WonAuctionVm FromAuction(Auction auction)
        {
            if (auction == null) throw new ArgumentNullException(nameof(auction));
            return new WonAuctionVm
            {
                Id = auction.Id,
                Title = auction.Title,
                EndDate = auction.EndDate,
                HighestBid = auction.Bids.Max(bid => bid.Amount)
            };
        }
    }
}