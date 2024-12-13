namespace Labb2Dissys.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllByUserName(string userName);

    List<Auction> GetAllActive();

    List<Auction> GetAllClosedWithHighestBidByUser(string userName);
    
    List<Auction> GetAllActiveWhereUserBid(string userName);
    Auction GetById(int id, string userName);
    
    Auction GetByIdOnly(int id);
    
    void Add(string userName, string title, decimal startingPrice, string description, DateTime? endDate = null); 
    void UpdateDescription(int auctionId, string description);

}