using Labb2Dissys.Core;

namespace ProjectApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllByUserName(string userName);  
    
    Auction GetById(int id, string userName);
    
    void Add(string userName, string Title);  
}