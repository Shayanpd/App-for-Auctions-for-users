using System.Data;
using AutoMapper;
using Labb2Dissys.Core;
using Labb2Dissys.Core.Interfaces;
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
                auction.Bids.Add(_mapper.Map<Bid>(bidDb));
            }

            return auction;
        }

        public void Save(Auction auction)
        {
            throw new NotImplementedException("Save");
        }
    }
}