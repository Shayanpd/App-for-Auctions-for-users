using AutoMapper;
using Labb2Dissys.Core;
using Labb2Dissys.Persistence;

namespace Labb2Dissys.Mappers
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<BidDb, Bid>().ReverseMap();
        }
    }
}