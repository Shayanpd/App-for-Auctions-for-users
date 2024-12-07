using System;
using System.Collections.Generic;

namespace Labb2Dissys.Core
{
    public class Auction
    {
        public int Id { get; set; } // Unique identifier for the auction
        public string Title { get; set; } = string.Empty; // Name of the auction
        public string Description { get; set; } = string.Empty; // Description of the auction
        public string Seller { get; set; } = string.Empty; // Email of the seller
        public decimal StartingPrice { get; set; } // The initial price for bidding
        public DateTime EndDate { get; set; } // The end date of the auction
        public List<Bid> Bids { get; set; } = new(); // List of bids associated with the auction

        /// <summary>
        /// Determines if the auction is currently active
        /// </summary>
        /// <returns>True if the auction is active, false otherwise</returns>
        public bool IsActive()
        {
            return EndDate > DateTime.Now; // Auction is still active if the end date is in the future
        }
    }
}