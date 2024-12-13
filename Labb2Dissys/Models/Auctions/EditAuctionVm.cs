using System.ComponentModel.DataAnnotations;

namespace Labb2Dissys.Models.Auctions
{
    public class EditAuctionVm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }
    }
}