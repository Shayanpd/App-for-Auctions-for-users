using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace Labb2Dissys.Models.Auctions;

public class CreateAuctionVm
{
    [Required]
    [StringLength(128, ErrorMessage = "max 128 characters")]
    public string? Title { get; set; }
}