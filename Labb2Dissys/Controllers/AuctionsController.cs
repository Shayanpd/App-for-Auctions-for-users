using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Labb2Dissys.Core;
using Labb2Dissys.Core.Interfaces;
using Labb2Dissys.Models.Auctions;

namespace Labb2Dissys.Controllers
{
    public class AuctionsController : Controller
    { 
        private IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        
        // GET: AuctionsController
        public ActionResult Index()
        {
            //List<Auction> auctions = _auctionService.GetAllByUserName( "user1@kth.se"/**User.Identity.Name**/);
            List<Auction> auctions = _auctionService.GetAllActive();
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            
            auctions = auctions.OrderBy(auction => auction.EndDate).ToList();
            
            foreach (var auction in auctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }

            return View(auctionVms);
        }
        
        public ActionResult ActiveBids()
        {
            string userName = User.Identity.Name; // Retrieve logged-in user's identity
            var allAuctions = _auctionService.GetAllActive(); // Fetch all active auctions
            var userActiveBids = allAuctions
                .Where(auction => auction.Bids.Any(bid => bid.Bidder == userName)) // Filter where user placed bids
                .OrderBy(auction => auction.EndDate)
                .ToList();

            var auctionVms = userActiveBids.Select(AuctionVm.FromAuction).ToList();
            return View(auctionVms);
        }
        
        public ActionResult WonAuctions()
        {
            string userName = User.Identity.Name; // Retrieve logged-in user's identity
            var allAuctions = _auctionService.GetAllClosed(); // Fetch all active auctions
            var userActiveBids = allAuctions
                .Where(auction => auction.Bids.Any(bid => bid.Bidder == userName)) // Filter where user placed bids
                .OrderBy(auction => auction.EndDate)
                .ToList();

            var wonAuctionVms = userActiveBids.Select(WonAuctionVm.FromAuction).ToList();
            return View(wonAuctionVms);
        }
        
        public ActionResult CurrentBidsForUser()
        {
            string userName = User.Identity.Name; // Retrieve logged-in user's identity
            var allAuctions = _auctionService.GetAllActive(); // Fetch all active auctions
            var userActiveBids = allAuctions
                .Where(auction => auction.Bids.Any(bid => bid.Bidder == userName)) // Filter where user placed bids
                .ToList();

            var wonAuctionVms = userActiveBids.Select(WonAuctionVm.FromAuction).ToList();
            return View(wonAuctionVms);
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            Console.WriteLine($"Received ID: {id}"); // Debugging
            try
            {
                //Auction auction = _auctionService.GetById(id, User.Identity.Name); // current user, hårdkodat ATM
                Auction auction = _auctionService.GetByIdOnly(id);
                if (auction == null) return BadRequest(); // HTTP 400

                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction);
                
                detailsVm.Bids = detailsVm.Bids.OrderByDescending(bid => bid.Amount).ToList();
                return View(detailsVm);
            }
            catch (DataException ex)
            {
                return BadRequest();
            }
        }
        
        
        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVm createAuctionVm)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string userName = User.Identity.Name; // Replace with User.Identity.Name if using authentication
                    decimal startingPrice = createAuctionVm.StartingPrice;
                    string description = createAuctionVm.Description ?? string.Empty;
                    DateTime? endDate = createAuctionVm.EndDate;

                    _auctionService.Add(userName, createAuctionVm.Title, startingPrice, description, endDate);
                    return RedirectToAction("Index");
                }
                return View(createAuctionVm);
            }
            catch(DataException ex)
            {
                return View(createAuctionVm);
            }
        }
        
        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    
    }
}
