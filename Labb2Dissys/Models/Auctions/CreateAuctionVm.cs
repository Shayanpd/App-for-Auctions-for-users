using System;
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

        [Required]
        [EndDateValidation(ErrorMessage = "EndDate must be at least 7 days from now")]
        public DateTime? EndDate { get; set; }
    }

    public class EndDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime endDate)
            {
                if (endDate < DateTime.Now.AddDays(7))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}