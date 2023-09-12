using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Repositories
{
    public class GameOrderRepository : Repository<GameOrder>, IGameOrderRepository
    {
        private readonly GameShopContext _context;

        public GameOrderRepository(GameShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddGameOrdersAsync(List<int> gameIds, Order order)
        {
            gameIds.RemoveAll(i => i == 0);

            foreach (var id in gameIds)
            {
                var gameOrder = new GameOrder { OrderNumber = order.OrderNumber, GameId = id, OrderId = order.Id, CreatedBy = order.CreatedBy };

                await _context.GameOrders.AddAsync(gameOrder);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameOrder> GetByGameIdAndOrderNumber(int gameId, string orderNumber)
        {
            return await _context.GameOrders.AsNoTracking().FirstOrDefaultAsync(go => go.GameId == gameId && go.OrderNumber == orderNumber);
        }

        public List<int> GetGameIdsByOrderId(int orderId)
        {
            return _context.GameOrders.AsNoTracking().Where(go => go.OrderId == orderId).Select(g => g.GameId).ToList();
        }

        public async Task UpdateGameOrderAsync(GameOrder gameOrder)
        {
            gameOrder.HasBeenReviewed = true;

            _context.GameOrders.Update(gameOrder);
            await _context.SaveChangesAsync();
        }
    }
}
