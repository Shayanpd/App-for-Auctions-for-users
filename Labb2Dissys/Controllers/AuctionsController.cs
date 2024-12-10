using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Labb2Dissys.Core;
using Labb2Dissys.Models.Auctions;
using ProjectApp.Core.Interfaces;

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
            List<Auction> auctions = _auctionService.GetAllByUserName( "user1@kth.se"/**User.Identity.Name**/);
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }

            return View(auctionVms);
        }

        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            Console.WriteLine($"Received ID: {id}"); // Debugging
            try
            {
                Auction auction = _auctionService.GetById(id, "user1@kth.se"/**User.Identity.Name**/); // current user, hårdkodat ATM
                if (auction == null) return BadRequest(); // HTTP 400

                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction);
                return View(detailsVm);
            }
            catch (DataException ex)
            {
                return BadRequest();
            }
        }

        /*
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
                    string name = createAuctionVm.Name;
                    string userName = User.Identity.Name;
                    _auctionService.Add(userName, name);
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
        */
    }
}
