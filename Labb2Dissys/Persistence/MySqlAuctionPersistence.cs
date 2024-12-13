using System.Data;
using AutoMapper;
using Labb2Dissys.Core;
using Labb2Dissys.Core.Interfaces;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;

namespace Labb2Dissys.Persistence
{
    public class MySqlAuctionPersistence : IAuctionPersistence
    {
        private readonly AuctionDbContext _dbContext;
        private readonly IMapper _mapper;

        public MySqlAuctionPersistence(AuctionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Auction> GetAllByUserName(string userName)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.Seller == userName)
                .Include(a => a.BidDbs)
                .ToList();

            return auctionDbs.Select(a => _mapper.Map<Auction>(a)).ToList();
        }
        
        public List<Auction> GetAllActive()
        {
            var now = DateTime.Now;
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.EndDate > now) // Add condition for active auctions
                .Include(a => a.BidDbs)
                .ToList();

            return auctionDbs.Select(a => _mapper.Map<Auction>(a)).ToList();
        }
        
        public List<Auction> GetAllClosedWithHighestBidByUser(string userName)
        {
            var now = DateTime.Now;
    
            // Get closed auctions and their bids, filtering to only include auctions where the user is the highest bidder
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.EndDate < now) // Get only closed auctions
                .Include(a => a.BidDbs) // Include associated bids
                .Where(a => a.BidDbs.Any(b => b.Bidder == userName)) // Ensure the user has placed a bid
                .ToList();

            // Now, filter further by checking if the user has the highest bid
            List<Auction> auctions = new List<Auction>();
    
            foreach (var auctionDb in auctionDbs)
            {
                // Get the highest bid for this auction
                var highestBid = auctionDb.BidDbs.OrderByDescending(b => b.Amount).FirstOrDefault();

                // Check if the highest bid belongs to the logged-in user
                if (highestBid != null && highestBid.Bidder == userName)
                {
                    // Map the auction and its bids to the Auction model
                    Auction auction = _mapper.Map<Auction>(auctionDb);

                    // Map and add all bids to the auction
                    foreach (var bidDb in auctionDb.BidDbs)
                    {
                        Bid bid = _mapper.Map<Bid>(bidDb);
                        auction.AddBid(bid); // Add bid to the auction
                    }

                    auctions.Add(auction); // Add auction to the result list
                }
            }

            return auctions; // Return the list of auctions where the user has the highest bid
        }


        public Auction GetById(int id, string userName)
        {
            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == id && a.Seller == userName)
                .Include(a => a.BidDbs)
                .FirstOrDefault();

            if (auctionDb == null) throw new DataException("Auction not found");
            
            Auction auction = _mapper.Map<Auction>(auctionDb);

            foreach (BidDb bidDb in auctionDb.BidDbs)
            {
                Bid bid = _mapper.Map<Bid>(bidDb);
                auction.AddBid(bid);
            }

            return auction;
        }
        public List<Auction> GetAllActiveWhereUserBid(string userName)
        {
            var now = DateTime.Now;

            // Print the user for whom we are fetching auctions
            Console.WriteLine($"Fetching active auctions for user: {userName}");

            // Get active auctions and their bids, filtering to only include auctions where the user placed at least one bid
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.EndDate > now) // Get only active auctions
                .Include(a => a.BidDbs) // Include associated bids
                .Where(a => a.BidDbs.Any(b => b.Bidder == userName)) // Ensure the user has placed at least one bid
                .ToList();

            // Print how many auctions we fetched
            Console.WriteLine($"Number of active auctions found for user '{userName}': {auctionDbs.Count}");

            List<Auction> auctions = new List<Auction>();

            // Loop through each auction and print the details
            foreach (var auctionDb in auctionDbs)
            {
                Console.WriteLine($"Processing auction: {auctionDb.Title} (ID: {auctionDb.Id})");

                // Print how many bids are there for the auction
                Console.WriteLine($"Auction '{auctionDb.Title}' has {auctionDb.BidDbs.Count} bids.");

                // Map auction
                Auction auction = _mapper.Map<Auction>(auctionDb);

                // Map and add all bids to the auction
                foreach (var bidDb in auctionDb.BidDbs)
                {
                    Bid bid = _mapper.Map<Bid>(bidDb);
                    auction.AddBid(bid); // Add bid to the auction

                    // Print out the details of each bid
                    Console.WriteLine($"Bid placed by '{bid.Bidder}' with amount: {bid.Amount}.");
                }

                auctions.Add(auction); // Add auction to the result list
            }

            // Return the list of active auctions where the user placed a bid
            return auctions;
        }

        
        public Auction GetByIdOnly(int id)
        {
            var auctionDb = _dbContext.AuctionDbs
                .Where(a => a.Id == id)
                .Include(a => a.BidDbs)
                .FirstOrDefault();

            if (auctionDb == null) throw new DataException("Auction not found");
            
            Auction auction = _mapper.Map<Auction>(auctionDb);

            foreach (BidDb bidDb in auctionDb.BidDbs)
            {
                Bid bid = _mapper.Map<Bid>(bidDb);
                auction.AddBid(bid);
                
            }

            return auction;
        }
        
        public void PutBid(int auctionId, string userName, decimal bidAmount)
        {
            // Fetch the auction from the database
            var auction = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .FirstOrDefault(a => a.Id == auctionId);

            // Create and add the new bid
            var newBid = new BidDb
            {
                AuctionId = auctionId,
                Bidder = userName,
                Amount = bidAmount,
                Timestamp = DateTime.Now
            };
            auction.BidDbs.Add(newBid);

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        public void Save(Auction auction)
        {
            AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
            _dbContext.AuctionDbs.Add(auctionDb);
            _dbContext.SaveChanges();
        }
        
        public void Update(Auction auction)
        {
            var existingAuction = _dbContext.AuctionDbs.Find(auction.Id);
            if (existingAuction == null)
            {
                throw new KeyNotFoundException("Auction not found.");
            }

            existingAuction.Description = auction.Description;
            _dbContext.SaveChanges();
        }

    }
}