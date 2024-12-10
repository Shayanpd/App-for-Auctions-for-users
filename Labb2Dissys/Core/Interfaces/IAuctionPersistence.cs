namespace Labb2Dissys.Core.Interfaces;


public interface IAuctionPersistence
{
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id, string userName);
    
    void Save(Auction project);
}