using AutoMapper;
using GameShop.Api.Models;
using GameShop.Api.Models.Dtos;

namespace GameShop.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<GameOrder, GameOrderDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
