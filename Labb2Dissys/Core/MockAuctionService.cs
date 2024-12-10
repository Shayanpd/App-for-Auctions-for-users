using Labb2Dissys.Core.Interfaces;
/**using ProjectApp.Core.Interfaces;

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
            Bids = new List<Bid>
            {
                new Bid { Id = 1, Amount = 10500m, Bidder = "bidder1@example.com", Timestamp = DateTime.Now.AddDays(-1) },
                new Bid { Id = 2, Amount = 11000m, Bidder = "bidder2@example.com", Timestamp = DateTime.Now.AddHours(-12) },
            }
        };

        Auction a2 = new Auction
        {
            Id = 2,
            Title = "Antique Vase Auction",
            Description = "Rare antique vases from China.",
            Seller = "user1@kth.se",
            StartingPrice = 2000m,
            EndDate = DateTime.Now.AddDays(14),
            Bids = new List<Bid>
            {
                new Bid { Id = 3, Amount = 2100m, Bidder = "bidder3@example.com", Timestamp = DateTime.Now.AddHours(-2) },
                new Bid { Id = 4, Amount = 2200m, Bidder = "bidder4@example.com", Timestamp = DateTime.Now.AddMinutes(-30) },
            }
        };

        _auctions.Add(a1);
        _auctions.Add(a2);
    }
}**/