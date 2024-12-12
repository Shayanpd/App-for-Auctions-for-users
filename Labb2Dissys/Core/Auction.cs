using System;
using System.Collections.Generic;

namespace Labb2Dissys.Core
{
    public class Auction
    {
        
        public Auction(string title, string userName, decimal startingPrice, string description, DateTime? endDate = null)
        {
            Title = title;
            Seller = userName;
            StartingPrice = startingPrice;
            Description = description;
            EndDate = endDate ?? DateTime.Now.AddDays(14);
        }
        
        public Auction() { }
        public int Id { get; set; } // Unique identifier for the auction
        public string Title { get; set; } = string.Empty; // Name of the auction
        
        public string Description { get; set; } = string.Empty; // Description of the auction
        public string Seller { get; set; } = string.Empty; // Email of the seller
        public decimal StartingPrice { get; set; } // The initial price for bidding
        public DateTime EndDate { get; set; } // The end date of the auction
        
        private List<Bid> _bids = new List<Bid>();
        public IEnumerable<Bid> Bids => _bids;
        
        public void AddBid(Bid newBid)
        {
            _bids.Add(newBid);
        }

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