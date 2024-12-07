using Labb2Dissys.Core.Interfaces;
using ProjectApp.Core.Interfaces;

namespace Labb2Dissys.Core;

public class MockAuctionService : IAuctionService
{
    public List<Auction> GetAllByUserName(string userName)
    {
        return _auctions;
    }

    public Auction GetById(int id, string userName)
    {
        return _auctions.Find(a => a.Id == id && a.Seller == userName);
    }

    public void Add(string userName, string name)
    {
        throw new NotImplementedException("MockAuctionService.Add");
    }

    private static readonly List<Auction> _auctions = new();

    // C# style static initializer
    static MockAuctionService()
    {
        Auction a1 = new Auction
        {
            Id = 1,
            Title = "Vintage Car Auction",
            Description = "Classic vintage cars from the 1950s.",
            Seller = "user1@kth.se",
            StartingPrice = 10000m,
            EndDate = DateTime.Now.AddDays(7),
        };

        Auction a2 = new Auction
        {
            Id = 2,
            Title = "Antique Vase Auction",
            Description = "Rare antique vases from China.",
            Seller = "user1@kth.se",
            StartingPrice = 2000m,
            EndDate = DateTime.Now.AddDays(14),
        };

        _auctions.Add(a1);
        _auctions.Add(a2);
    }
}