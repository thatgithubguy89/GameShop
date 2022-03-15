using AutoMapper;
using GameShop.Models;
using GameShop.Models.Dtos;

namespace GameShop.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game,GameDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
