﻿using System.Data;
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

        /// <summary>
        /// Retrieves auction details by ID for a given seller.
        /// </summary>
        public Auction GetById(int id, string userName)
        {
            Auction auction = _auctionPersistence.GetById(id, userName);
            if (auction == null) throw new DataException("Auction not found");
            return auction;
        }

        /// <summary>
        /// Adds a new auction to the database.
        /// </summary>
        public void Add(string userName, string title)
        {
            if(userName == null) throw new DataException("User name is required");
            if(title == null || title.Length > 128) throw new DataException("Title is required and cannot be longer than 128 characters");
            
            Auction auction = new Auction(title, userName);
            _auctionPersistence.Save(auction);
        }
    }
}