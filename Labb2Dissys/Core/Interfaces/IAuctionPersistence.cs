namespace Labb2Dissys.Core.Interfaces;


public interface IAuctionPersistence
{
    List<Auction> GetAllByUserName(string userName);
    
    List<Auction> GetAllActive();
    
    List<Auction> GetAllClosedWithHighestBidByUser(string userName);
    
    List<Auction> GetAllActiveWhereUserBid(string userName);
    
    Auction GetById(int id, string userName);

    Auction GetByIdOnly(int id);
    
    void Save(Auction project);
    
    void PutBid(int auctionId, string userName, decimal bidAmount);
    
    void Update(Auction auction);

}