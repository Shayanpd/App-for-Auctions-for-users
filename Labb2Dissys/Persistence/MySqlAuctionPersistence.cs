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
        
        public List<Auction> GetAllClosed()
        {
            var now = DateTime.Now;
            var auctionDbs = _dbContext.AuctionDbs
                .Where(a => a.EndDate < now) // Filter auctions that have ended
                .Include(a => a.BidDbs) // Include associated bids
                .ToList();

            // Map auctions and their bids individually
            List<Auction> auctions = new List<Auction>();

            foreach (var auctionDb in auctionDbs)
            {
                // Map auction
                Auction auction = _mapper.Map<Auction>(auctionDb);

                // Map bids for the auction
                foreach (BidDb bidDb in auctionDb.BidDbs)
                {
                    Bid bid = _mapper.Map<Bid>(bidDb);
                    auction.AddBid(bid); // Add bid to the auction
                }

                auctions.Add(auction); // Add auction to the result list
            }

            return auctions; // Return the fully mapped list of auctions
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

        public void Save(Auction auction)
        {
            AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
            _dbContext.AuctionDbs.Add(auctionDb);
            _dbContext.SaveChanges();
        }
    }
}