using GameShop.Api.Models.Responses;

namespace GameShop.Api.Interfaces
{
    public interface IPageService<DbModel, DtoModel> where DbModel : class where DtoModel : class
    {
        PageResponse<DtoModel> Page(List<DbModel> items, int startingIndex = 1, float pageTotal = 9);
    }
}
