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
            var allWonAuctions = _auctionService.GetAllClosedWithHighestBidByUser(userName); // Fetch all active auctions
            /**var userActiveBids = allAuctions
                .Where(auction => auction.Bids.Any(bid => bid.Bidder == userName)) // Filter where user placed bids
                .OrderBy(auction => auction.EndDate)
                .ToList();**/

            var wonAuctionVms = allWonAuctions.Select(WonAuctionVm.FromAuction).ToList();
            return View(wonAuctionVms);
        }
        
        public ActionResult UserBids()
        {
            string userName = User.Identity.Name; // Retrieve logged-in user's identity
            Console.WriteLine($"Fetching active auctions for user: {userName}"); // Print user name

            // Fetch all active auctions where the user has placed a bid
            var allActiveAuctionsWhereUserBid = _auctionService.GetAllActiveWhereUserBid(userName); 
            Console.WriteLine($"Number of active auctions with bids from '{userName}': {allActiveAuctionsWhereUserBid.Count}");

            // Filter auctions where the user has placed a bid
            

            // Convert the filtered auctions into the ViewModel
            var userBidsVms = allActiveAuctionsWhereUserBid.Select(UserBidsVm.FromAuction).ToList();

            // Log the number of ViewModels created
            Console.WriteLine($"Number of ViewModels created: {userBidsVms.Count}");

            // Return the View with the user bids ViewModels
            return View(userBidsVms);
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
            // Retrieve the auction by ID
            var auction = _auctionService.GetByIdOnly(id);

            // Ensure the auction exists
            if (auction == null)
            {
                return NotFound();
            }

            // Ensure the current user is the owner
            if (auction.Seller != User.Identity.Name)
            {
                return Forbid(); // HTTP 403 Forbidden
            }

            // Create a view model with only the Description field
            var editAuctionVm = new EditAuctionVm
            {
                Id = auction.Id,
                Description = auction.Description
            };

            return View(editAuctionVm);
        }
        
        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditAuctionVm editAuctionVm)
        {
            if (!ModelState.IsValid)
            {
                return View(editAuctionVm); // Return the form with validation errors
            }

            try
            {
                // Retrieve the auction by ID
                var auction = _auctionService.GetByIdOnly(id);

                // Ensure the auction exists
                if (auction == null)
                {
                    return NotFound();
                }

                // Ensure the current user is the owner
                if (auction.Seller != User.Identity.Name)
                {
                    return Forbid(); // HTTP 403 Forbidden
                }

                // Update the description via the service
                _auctionService.UpdateDescription(id, editAuctionVm.Description);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(editAuctionVm);
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
