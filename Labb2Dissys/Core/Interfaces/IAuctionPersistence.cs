namespace Labb2Dissys.Core.Interfaces;


public interface IProjectPersistence
{
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id, string userName);
    
    void Save(Auction project);
}