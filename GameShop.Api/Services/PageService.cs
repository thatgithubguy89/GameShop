using AutoMapper;
using GameShop.Api.Interfaces;
using GameShop.Api.Models.Responses;

namespace GameShop.Api.Services
{
    public class PageService<DbModel, DtoModel> : IPageService<DbModel, DtoModel> where DbModel : class where DtoModel : class
    {
        private readonly IMapper _mapper;

        public PageService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PageResponse<DtoModel> Page(List<DbModel> items, int startingIndex = 1, float pageTotal = 9)
        {
            var pageCount = Math.Ceiling(items.Count / pageTotal);

            items = items
                .Skip((startingIndex - 1) * (int)pageTotal)
                .Take((int)pageTotal)
                .ToList();

            return new PageResponse<DtoModel>
            {
                Payload = _mapper.Map<List<DtoModel>>(items),
                StartingIndex = startingIndex,
                PageTotal = (int)pageCount
            };
        }
    }
}
