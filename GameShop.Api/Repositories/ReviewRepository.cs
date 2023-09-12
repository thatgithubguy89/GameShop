using GameShop.Api.Data;
using GameShop.Api.Interfaces;
using GameShop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Api.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly GameShopContext _context;
        private readonly ICacheService<Review> _cacheService;

        public ReviewRepository(GameShopContext context, ICacheService<Review> cacheService) : base(context)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<List<Review>> GetReviewsByGameIdAsync(int gameId)
        {
            var reviews = await _cacheService.GetItemsAsync($"game-{gameId}-reviews");
            if (reviews != null)
                return reviews;

            reviews = await _context.Reviews.Where(r => r.GameId == gameId).ToListAsync();

            await _cacheService.SetItemsAsync(reviews, $"game-{gameId}-reviews");

            return reviews;
        }
    }
}
