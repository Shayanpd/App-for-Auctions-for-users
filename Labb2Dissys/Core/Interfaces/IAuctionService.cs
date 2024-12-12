namespace Labb2Dissys.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllByUserName(string userName);  
    
    Auction GetById(int id, string userName);
    
    void Add(string userName, string title, decimal startingPrice, string description, DateTime? endDate = null); 
}