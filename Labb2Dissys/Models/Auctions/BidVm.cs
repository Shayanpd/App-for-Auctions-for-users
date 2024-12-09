using System.ComponentModel.DataAnnotations;
using Labb2Dissys.Core;

namespace Labb2Dissys.Models.Auctions
{
    public class BidVm
    {
        public int Id { get; set; }

        [Display(Name = "Bidder's Name")]
        [Required]
        public string Bidder { get; set; } = string.Empty;

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Bid Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime Timestamp { get; set; }

        public static BidVm FromBid(Bid bid)
        {
            if (bid == null) throw new ArgumentNullException(nameof(bid));
            return new BidVm
            {
                Id = bid.Id,
                Bidder = bid.Bidder,
                Amount = bid.Amount,
                Timestamp = bid.Timestamp
            };
        }
    }
}