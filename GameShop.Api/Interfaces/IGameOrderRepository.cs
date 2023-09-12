using GameShop.Api.Models;

namespace GameShop.Api.Interfaces
{
    public interface IGameOrderRepository : IRepository<GameOrder>
    {
        Task AddGameOrdersAsync(List<int> gameIds, Order order);
        Task<GameOrder> GetByGameIdAndOrderNumber(int gameId, string orderNumber);
        List<int> GetGameIdsByOrderId(int orderId);
        Task UpdateGameOrderAsync(GameOrder gameOrder);
    }
}
