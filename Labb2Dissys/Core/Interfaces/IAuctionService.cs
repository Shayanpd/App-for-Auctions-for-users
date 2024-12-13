namespace Labb2Dissys.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllByUserName(string userName);

    List<Auction> GetAllActive();
    
    List<Auction> GetAllClosed();
    Auction GetById(int id, string userName);
    
    Auction GetByIdOnly(int id);
    
    void Add(string userName, string title, decimal startingPrice, string description, DateTime? endDate = null); 
}