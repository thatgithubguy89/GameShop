using GameShop.Api.Models;

namespace GameShop.Api.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<List<Game>> GetGamesByIdsAsync(List<int> ids);
    }
}
