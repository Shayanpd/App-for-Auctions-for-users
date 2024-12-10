using AutoMapper;
using Labb2Dissys.Core;
using Labb2Dissys.Persistence;


namespace Labb2Dissys.Mappers
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<AuctionDb, Auction>().ReverseMap();
        }
    }
}