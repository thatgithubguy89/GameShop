using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly GameShopContext _context;
        private readonly IGameOrderRepository _gameOrderRepository;

        public OrderRepository(GameShopContext context, IGameOrderRepository gameOrderRepository) : base(context)
        {
            _context = context;
            _gameOrderRepository = gameOrderRepository;
        }

        public async Task<Order> AddOrderAsync(Order order, List<int> gameIds)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (gameIds.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(gameIds));

            order.OrderNumber = Guid.NewGuid().ToString();

            var createdOrder = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            await _gameOrderRepository.AddGameOrdersAsync(gameIds, createdOrder.Entity);

            return createdOrder.Entity;
        }

        public override async Task<Order> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var order = await _context.Orders.Include(o => o.GameOrders)
                                             .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new NullReferenceException(nameof(order));

            return order;
        }
    }
}
