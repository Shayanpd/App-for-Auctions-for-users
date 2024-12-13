using System.Data;
using Labb2Dissys.Core.Interfaces;

namespace Labb2Dissys.Core
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionPersistence _auctionPersistence;

        public AuctionService(IAuctionPersistence auctionPersistence)
        {
            _auctionPersistence = auctionPersistence;
        }

        /// <summary>
        /// Retrieves all auctions by the seller's username.
        /// </summary>
        public List<Auction> GetAllByUserName(string userName)
        {
            List<Auction> auctions = _auctionPersistence.GetAllByUserName(userName);
            return auctions;
        }

        public List<Auction> GetAllActive()
        {
            List<Auction> auctions = _auctionPersistence.GetAllActive();
            return auctions;
        }
        
        public List<Auction> GetAllClosedWithHighestBidByUser(string userName)
        {
            List<Auction> auctions = _auctionPersistence.GetAllClosedWithHighestBidByUser(userName);
            return auctions;
        }
        
        public List<Auction> GetAllActiveWhereUserBid(string userName)
        {
            List<Auction> auctions = _auctionPersistence.GetAllActiveWhereUserBid(userName);
            return auctions;
        }
        

        /// <summary>
        /// Retrieves auction details by ID for a given seller.
        /// </summary>
        public Auction GetById(int id, string userName)
        {
            Auction auction = _auctionPersistence.GetById(id, userName);
            if (auction == null) throw new DataException("Auction not found");
            return auction;
        }

        public Auction GetByIdOnly(int id)
        {
            Auction auction = _auctionPersistence.GetByIdOnly(id);
            if (auction == null) throw new DataException("Auction not found");
            return auction;
        }

        /// <summary>
        /// Adds a new auction to the database.
        /// </summary>
        public void Add(string userName, string title, decimal startingPrice, string description, DateTime? endDate = null)
        {
            if (userName == null) throw new DataException("User name is required");
            if (title == null || title.Length > 128) throw new DataException("Title is required and cannot be longer than 128 characters");
            if (startingPrice <= 0) throw new DataException("Starting price must be greater than 0");

            Auction auction = new Auction(title, userName, startingPrice, description, endDate);
            _auctionPersistence.Save(auction);
        }

        public void PutBid(int auctionId, string userName, decimal bidAmount)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name is required.", nameof(userName));

            if (bidAmount <= 0)
                throw new ArgumentException("Bid amount must be greater than zero.", nameof(bidAmount));

            // Fetch the auction by ID
            var auction = _auctionPersistence.GetByIdOnly(auctionId);
            if (auction == null)
                throw new DataException("Auction not found.");

            if (auction.Seller == userName)
                throw new InvalidOperationException("You cannot bid on your own auction.");

            if (auction.EndDate <= DateTime.Now)
                throw new InvalidOperationException("The auction has already ended.");

            // Get the highest bid or the starting price
            var highestBid = auction.Bids.Max(b => (decimal?)b.Amount) ?? auction.StartingPrice;

            if (bidAmount <= highestBid)
                throw new InvalidOperationException("Your bid must be higher than the current highest bid or starting price.");

            // Delegate to the persistence layer to save the bid
            _auctionPersistence.PutBid(auctionId, userName, bidAmount);
        }
        
        public void UpdateDescription(int auctionId, string description)
        {
            var auction = _auctionPersistence.GetByIdOnly(auctionId);

            if (auction == null)
            {
                throw new KeyNotFoundException("Auction not found.");
            }

            auction.Description = description;
            _auctionPersistence.Update(auction);
        }
        

    }
}