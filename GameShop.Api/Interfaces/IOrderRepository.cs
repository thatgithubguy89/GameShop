using GameShop.Api.Models;

namespace GameShop.Api.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> AddOrderAsync(Order order, List<int> gameIds);
    }
}
