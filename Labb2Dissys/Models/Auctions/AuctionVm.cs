using System.ComponentModel.DataAnnotations;
using Labb2Dissys.Core;

namespace Labb2Dissys.Models.Auctions
{
    public class AuctionVm
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Title { get; set; }
        
        [Display(Name = "Created date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EndDate { get; set; }

       // public bool IsActive { get; set; }

        public static AuctionVm FromAuction(Auction auction)
        {
            return new AuctionVm()
            {
                Id = auction.Id,
                Title = auction.Title,
                EndDate = auction.EndDate,
            };
        }
    }
}